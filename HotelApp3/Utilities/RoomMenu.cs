using HotelApp3.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp3.Utilities
{
    public class RoomMenu
    {
        private readonly RoomController _controller;

        public RoomMenu(RoomController controller)
        {
            _controller = controller;
        }

        public void ShowMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("--- Rumhantering ---");
                Console.WriteLine("1. Lägg till rum");
                Console.WriteLine("2. Visa alla rum");
                Console.WriteLine("3. Uppdatera rum");
                Console.WriteLine("4. Ta bort rum");
                Console.WriteLine("5. Lägg till extrasäng");
                Console.WriteLine("6. Ta bort extrasäng");
                Console.WriteLine("7. Gå tillbaka till huvudmenyn");
                Console.Write("Välj ett alternativ: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        _controller.AddRoom();
                        break;
                    case "2":
                        _controller.ViewAllRooms();
                        Console.WriteLine("Tryck på valfri tangent för att återgå.");
                        Console.ReadKey();
                        break;
                    case "3":
                        _controller.UpdateRoom();
                        break;
                    case "4":
                        _controller.DeleteRoom();
                        break;
                    case "5":
                        _controller.AddExtraBed();
                        break;
                    case "6":
                        _controller.RemoveExtraBed();
                        break;
                    case "7":
                        back = true;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                        System.Threading.Thread.Sleep(1000);
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}
