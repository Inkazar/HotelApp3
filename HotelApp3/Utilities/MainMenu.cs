using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp3.Utilities
{
    public class MainMenu
    {
        private readonly CustomerMenu _customerMenu;
        private readonly RoomMenu _roomMenu;
        private readonly BookingMenu _bookingMenu;

        public MainMenu(CustomerMenu customerMenu, RoomMenu roomMenu, BookingMenu bookingMenu)
        {
            _customerMenu = customerMenu;
            _roomMenu = roomMenu;
            _bookingMenu = bookingMenu;
        }

        public void ShowMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("--- Huvudmeny ---");
                Console.WriteLine("1. Hantera kunder");
                Console.WriteLine("2. Hantera rum");
                Console.WriteLine("3. Hantera bokningar");
                Console.WriteLine("4. Avsluta");
                Console.Write("Välj ett alternativ: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        _customerMenu.ShowMenu();
                        break;
                    case "2":
                        _roomMenu.ShowMenu();
                        break;
                    case "3":
                        _bookingMenu.ShowMenu();
                        break;
                    case "4":
                        back = true;
                        Console.WriteLine("Avslutar...");
                        Console.ReadKey();
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
