using HotelApp3.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp3.Utilities
{
    public class CustomerMenu
    {
        private readonly CustomerController _controller;

        public CustomerMenu(CustomerController controller)
        {
            _controller = controller;
        }

        public void ShowMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("--- Kundhantering ---");
                Console.WriteLine("1. Lägg till kund");
                Console.WriteLine("2. Visa alla kunder");
                Console.WriteLine("3. Uppdatera kund");
                Console.WriteLine("4. Ta bort kund");
                Console.WriteLine("5. Gå tillbaka till huvudmenyn");
                Console.Write("Välj ett alternativ: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        _controller.AddCustomer();
                        break;
                    case "2":
                        _controller.ViewAllCustomers();
                        break;
                    case "3":
                        _controller.UpdateCustomer();
                        break;
                    case "4":
                        _controller.DeleteCustomer();
                        break;
                    case "5":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
