using HotelApp3.Models;
using HotelApp3.Services;
using System;
using System.Collections.Generic;
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

            string type;
            do
            {
                Console.Write("Ange typ (Single/Double): ");
                type = Console.ReadLine()?.ToLower();
            } while (type != "single" && type != "double");

            int maxCapacity;
            do
            {
                Console.Write("Ange maxkapacitet: ");
                if (type == "single" && int.TryParse(Console.ReadLine(), out maxCapacity) && maxCapacity == 1)
                {
                    break;
                }
                else if (type == "double" && int.TryParse(Console.ReadLine(), out maxCapacity) && maxCapacity > 1)
                {
                    break;
                }
                Console.WriteLine(type == "single" ? "Ett singelrum kan endast ha kapacitet 1." : "Ett dubbelrum måste ha kapacitet större än 1.");
            } while (true);

            decimal pricePerNight;
            do
            {
                Console.Write("Ange pris per natt: ");
            } while (!decimal.TryParse(Console.ReadLine(), out pricePerNight));

            var newRoom = new Room { Type = type, MaxCapacity = maxCapacity, PricePerNight = pricePerNight, ExtraBeds = 0 };
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
                Console.WriteLine($"ID: {room.RoomId}, Typ: {room.Type}, Kapacitet: {room.MaxCapacity}, Pris/Natt: {room.PricePerNight:C}, Extrasängar: {room.ExtraBeds}");
            }
            Console.WriteLine("Tryck på valfri tangent.");
            Console.ReadKey();
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

            Console.WriteLine($"Nuvarande typ: {room.Type}");
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
            room.Type = string.IsNullOrWhiteSpace(newType) ? room.Type : newType;

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
                if (int.TryParse(input, out newCapacity) && newCapacity > 0 && (room.Type != "single" || newCapacity == 1))
                {
                    break;
                }
                Console.WriteLine(room.Type == "single" ? "Ett singelrum kan endast ha kapacitet 1." : "Kapaciteten måste vara ett positivt heltal.");
            } while (true);
            room.MaxCapacity = newCapacity;

            Console.WriteLine($"Nuvarande pris: {room.PricePerNight:C}");
            Console.Write("Ange nytt pris (tryck Enter för att behålla): ");
            if (decimal.TryParse(Console.ReadLine(), out decimal newPrice) && newPrice > 0)
            {
                room.PricePerNight = newPrice;
            }

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

            _roomService.DeleteRoom(roomId);
            Console.WriteLine("Rummet har tagits bort!");
            Console.ReadKey();
        }

        public void AddExtraBed()
        {
            Console.Clear();
            Console.WriteLine("--- Lägg till extrasäng ---");
            ViewAllRooms();

            int roomId;
            do
            {
                Console.Write("Ange ID för rummet: ");
            } while (!int.TryParse(Console.ReadLine(), out roomId));

            var room = _roomService.GetRoomById(roomId);
            if (room == null || room.MaxCapacity <= 2)
            {
                Console.WriteLine("Extrasäng kan endast läggas till i rum med kapacitet större än 2.");
                Console.ReadKey();
                return;
            }

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
