﻿using HotelApp3.Models;
using HotelApp3.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp3.Utilities.Functions
{
    public class RoomFunction
    {
        private readonly RoomService _roomService;

        public RoomFunction(RoomService roomService)
        {
            _roomService = roomService;
        }

        public void AddRoom()
        {
            Console.Clear();
            Console.WriteLine("--- Lägg till rum ---");

            string roomType;
            do
            {
                Console.Write("Ange rumstyp (Single/Double): ");
                roomType = Console.ReadLine()?.ToLower();
                if (roomType != "single" && roomType != "double")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Felaktig inmatning. Du måste ange 'Single' eller 'Double'.");
                    Console.ResetColor();
                    roomType = null;
                }
            } while (roomType == null);

            int maxCapacity;
            do
            {
                Console.Write("Ange maxkapacitet: ");
                if (roomType == "single" && int.TryParse(Console.ReadLine(), out maxCapacity) && maxCapacity == 1)
                {
                    break;
                }
                else if (roomType == "double" && int.TryParse(Console.ReadLine(), out maxCapacity) && maxCapacity > 1)
                {
                    break;
                }
                Console.WriteLine(roomType == "single" ? "Ett singelrum kan endast ha kapacitet 1." : "Ett dubbelrum måste ha kapacitet större än 1.");
            } while (true);

            decimal pricePerNight;
            do
            {
                Console.Write("Ange pris per natt: ");
            } while (!decimal.TryParse(Console.ReadLine(), out pricePerNight));

            var newRoom = new Room { RoomType = roomType, MaxCapacity = maxCapacity, PricePerNight = pricePerNight, ExtraBeds = 0 };
            _roomService.AddRoom(newRoom);
            Console.WriteLine("Rummet har lagts till!");
            Console.ReadKey();
        }

        public void ViewAllRooms()
        {
            Console.Clear();
            Console.WriteLine("--- Alla rum ---");

            var rooms = _roomService.GetAllRooms().ToList();
            foreach (var room in rooms)
            {
                Console.WriteLine($"ID: {room.RoomId}, Typ: {room.RoomType}, Kapacitet: {room.MaxCapacity}, Pris/Natt: {room.PricePerNight:C}, Extrasängar: {room.ExtraBeds}");
            }
            Console.WriteLine("===========================");
        }

        public void UpdateRoom()
        {
            Console.Clear();
            Console.WriteLine("--- Uppdatera rum ---");
            ViewAllRooms();

            int roomId;
            do
            {
                Console.Write("Ange ID för rummet att uppdatera: ");
            } while (!int.TryParse(Console.ReadLine(), out roomId));

            var room = _roomService.GetRoomById(roomId);
            if (room == null)
            {
                Console.WriteLine("Rummet hittades inte.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Nuvarande typ: {room.RoomType}");
            string newType;
            do
            {
                Console.Write("Ange ny typ (Single/Double eller tryck Enter för att behålla): ");
                newType = Console.ReadLine()?.ToLower();
                if (string.IsNullOrWhiteSpace(newType) || newType == "single" || newType == "double")
                {
                    break;
                }
                Console.WriteLine("Ogiltig typ. Du måste ange 'single' eller 'double'.");
            } while (true);
            room.RoomType = string.IsNullOrWhiteSpace(newType) ? room.RoomType : newType;

            Console.WriteLine($"Nuvarande kapacitet: {room.MaxCapacity}");
            int newCapacity;
            do
            {
                Console.Write("Ange ny kapacitet (endast siffror, tryck Enter för att behålla): ");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    newCapacity = room.MaxCapacity;
                    break;
                }
                if (int.TryParse(input, out newCapacity) && newCapacity > 0 && (room.RoomType != "single" || newCapacity == 1))
                {
                    break;
                }
                Console.WriteLine(room.RoomType == "single" ? "Ett singelrum kan endast ha kapacitet 1." : "Kapaciteten måste vara ett positivt heltal.");
            } while (true);
            room.MaxCapacity = newCapacity;

            Console.WriteLine($"Nuvarande pris: {room.PricePerNight:C}");
            decimal newPrice;
            while (true)
            {
                Console.Write("Ange nytt pris (tryck Enter för att behålla): ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    newPrice = room.PricePerNight;
                    break;
                }
                if (decimal.TryParse(input, out newPrice) && newPrice > 0)
                {
                    break;
                }
                Console.WriteLine("Felaktigt pris. Ange ett giltigt numeriskt värde större än 0 eller tryck Enter för att behålla det nuvarande priset.");
            }

            room.PricePerNight = newPrice;
            _roomService.UpdateRoom(room);
            Console.WriteLine("Rumsuppgifterna har uppdaterats.");
            Console.ReadKey();
        }

        public void DeleteRoom()
        {
            Console.Clear();
            Console.WriteLine("--- Ta bort rum ---");
            ViewAllRooms();

            int roomId;
            do
            {
                Console.Write("Ange ID för rummet att ta bort: ");
            } while (!int.TryParse(Console.ReadLine(), out roomId));

            string confirmation;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("--- Bekräfta borttagning av Rum ---");
                Console.WriteLine($"Är du säker på att du vill ta bort: {roomId}? (ja/nej)");
                Console.ResetColor();
                confirmation = Console.ReadLine()?.Trim().ToLower();
                if (confirmation == "nej")
                {
                    Console.WriteLine("Åtgärden avbröts. Rummet har inte tagits bort.");
                    Console.ReadKey();
                    return;
                }
                else if (confirmation != "ja" && confirmation != "nej")
                {
                    Console.WriteLine("Felaktig inmatning. Ange 'ja' för att bekräfta eller 'nej' för att avbryta.");
                }
            } while (confirmation != "ja");

        

        _roomService.DeleteRoom(roomId);
            Console.WriteLine("Rummet har tagits bort!");
            Console.ReadKey();
        }

        public void AddExtraBed()
        {
            Console.Clear();
            Console.WriteLine("--- Lägg till extrasäng ---");

            int roomId;
            Room room;
            do
            {
                ViewAllRooms();
                Console.Write("Ange ID för rummet: ");
                roomId = int.TryParse(Console.ReadLine(), out int id) ? id : -1;
                room = _roomService.GetRoomById(roomId);
                if (room == null || room.MaxCapacity <= 2)
                {
                    Console.WriteLine("Extrasäng kan endast läggas till i rum med kapacitet större än 2. Försök igen.");
                    Console.ReadKey();
                }
            } while (room == null || room.MaxCapacity <= 2);

            room.ExtraBeds++;
            _roomService.UpdateRoom(room);
            Console.WriteLine("Extrasäng har lagts till!");
            Console.ReadKey();
        }

        public void RemoveExtraBed()
        {
            Console.Clear();
            Console.WriteLine("--- Ta bort extrasäng ---");
            ViewAllRooms();

            int roomId;
            do
            {
                Console.Write("Ange ID för rummet: ");
            } while (!int.TryParse(Console.ReadLine(), out roomId));

            var room = _roomService.GetRoomById(roomId);
            if (room == null || room.ExtraBeds <= 0)
            {
                Console.WriteLine("Det finns inga extrasängar att ta bort.");
                Console.ReadKey();
                return;
            }

            room.ExtraBeds--;
            _roomService.UpdateRoom(room);
            Console.WriteLine("Extrasäng har tagits bort!");
            Console.ReadKey();
        }
    }
}
