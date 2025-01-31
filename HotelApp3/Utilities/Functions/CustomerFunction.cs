using HotelApp3.Models;
using HotelApp3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HotelApp3.Utilities.Functions
{
    public class CustomerFunction
    {
        private readonly CustomerService _customerService;

        public CustomerFunction(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public string GetValidCustomerName()
        {
            string name;
            do
            {
                Console.Write("Ange kundens namn (för- och efternamn): ");
                name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name) || !Regex.IsMatch(name, @"^[a-zA-Z]+\s[a-zA-Z]+$"))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ogiltigt namn. Namnet måste innehålla för- och efternamn och får inte innehålla siffror.");
                    Console.ResetColor();
                    name = null;
                }
            } while (name == null);
            return name;
        }

        public string GetValidEmail()
        {
            string email;
            do
            {
                Console.Write("Ange kundens e-post: ");
                email = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ogiltig e-postadress. Försök igen.");
                    Console.ResetColor();
                    email = null;
                }
            } while (email == null);
            return email;
        }

        public string GetValidPhoneNumber()
        {
            string phone;
            do
            {
                Console.Write("Ange kundens telefonnummer: ");
                phone = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(phone) || !Regex.IsMatch(phone, @"^\d{7,15}$"))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ogiltigt telefonnummer. Ange endast siffror (7-15 tecken).");
                    Console.ResetColor();
                    phone = null;
                }
            } while (phone == null);
            return phone;
        }

        public int AddCustomer()
        {
            Console.Clear();
            Console.WriteLine("--- Lägg till kund ---");

            string name = GetValidCustomerName();
            string email = GetValidEmail();
            string phone = GetValidPhoneNumber();

            var newCustomer = new Customer { Name = name, Email = email, Phone = phone };
            _customerService.AddCustomer(newCustomer);
            Console.WriteLine("Kunden har lagts till!");
            Console.ReadKey();
            return newCustomer.CustomerId;
        }

        public void ViewAllCustomers()
        {
            Console.Clear();
            Console.WriteLine("--- Alla kunder ---");

            var customers = _customerService.GetAllCustomers().ToList();
            foreach (var customer in customers)
            {
                Console.WriteLine($"ID: {customer.CustomerId}, Namn: {customer.Name}, E-post: {customer.Email}, Telefon: {customer.Phone}");
            }
            Console.WriteLine("===========================");
            Console.WriteLine("Tryck på valfri tangent.");
            Console.ReadKey();
        }

        public void UpdateCustomer()
        {
            Console.Clear();
            Console.WriteLine("--- Uppdatera kund ---");
            ViewAllCustomers();

            int customerId;
            do
            {
                Console.Write("Ange ID för kunden att uppdatera: ");
            } while (!int.TryParse(Console.ReadLine(), out customerId));

            var customer = _customerService.GetCustomerById(customerId);
            if (customer == null)
            {
                Console.WriteLine("Kunden hittades inte.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Nuvarande namn: {customer.Name}");
            string newName;
            do
            {
                Console.Write("Ange nytt namn, för- och efternamn (eller tryck enter för att behålla): ");
                newName = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(newName))
                {
                    newName = customer.Name;
                    break;
                }
                if (!Regex.IsMatch(newName, @"^[a-zA-Z]+\s[a-zA-Z]+$"))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Felaktigt namn. Ange ett giltigt för- och efternamn utan siffror eller specialtecken.");
                    Console.ResetColor();
                    newName = null;
                }
            } while (newName == null);


            Console.WriteLine($"Nuvarande e-post: {customer.Email}");
            
            string newEmail;
            do
            {
                Console.Write("Ange ny e-post (tryck Enter för att behålla): ");
                newEmail = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(newEmail))
                {
                    newEmail = customer.Email;
                    break;
                }
                if (!Regex.IsMatch(newEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Felaktig e-postadress. Ange en giltig e-postadress eller tryck Enter för att behålla den befintliga.");
                    Console.ResetColor();
                    newEmail = null;
                }
            } while (newEmail == null);

            Console.WriteLine($"Nuvarande telefonnummer: {customer.Phone}");
            string newPhone;
            do
            {
                Console.Write("Ange nytt telefonnummer (tryck Enter för att behålla): ");
                newPhone = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(newPhone))
                {
                    newPhone = customer.Phone;
                    break;
                }
                if (!Regex.IsMatch(newPhone, @"^\d{7,15}$"))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Felaktigt telefonnummer. Ange ett giltigt nummer (7-15 siffror) eller tryck Enter för att behålla det befintliga.");
                    Console.ResetColor();
                    newPhone = null;
                }
            } while (newPhone == null);


            _customerService.UpdateCustomer(customer);
            Console.WriteLine("Kunduppgifterna har uppdaterats!");
            Console.ReadKey();
        }

        public void DeleteCustomer()
        {
            Console.Clear();
            Console.WriteLine("--- Ta bort kund ---");
            ViewAllCustomers();
            Console.Write("Ange kundens ID att ta bort: ");
            int customerId;
            while (!int.TryParse(Console.ReadLine(), out customerId) || customerId <= 0)
            {
                Console.Clear();
               
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Felaktigt ID. Ange ett giltigt numeriskt kund-ID.");
                Console.ResetColor();
                ViewAllCustomers();
                Console.Write("Ange kundens ID att ta bort: ");
            }

            var customer = _customerService.GetCustomerById(customerId);
            if (customer == null)
            {
                Console.WriteLine("Kunden hittades inte.");
                Console.ReadKey();
                return;
            }

            string confirmation;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("--- Bekräfta borttagning av kund ---");
                Console.WriteLine($"Är du säker på att du vill ta bort kunden: {customer.Name}? (ja/nej)");
                Console.ResetColor();
                confirmation = Console.ReadLine()?.Trim().ToLower();
                if (confirmation == "nej")
                {
                    Console.WriteLine("Åtgärden avbröts. Kunden har inte tagits bort.");
                    Console.ReadKey();
                    return;
                }
                else if (confirmation != "ja" && confirmation != "nej")
                {
                    Console.WriteLine("Felaktig inmatning. Ange 'ja' för att bekräfta eller 'nej' för att avbryta.");
                }
            } while (confirmation != "ja");

            _customerService.DeleteCustomer(customerId);
            Console.WriteLine("Kunden har tagits bort!");
            Console.ReadKey();
        }
    }
}
