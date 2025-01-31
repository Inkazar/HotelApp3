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
                    Console.WriteLine("Ogiltigt namn. Namnet måste innehålla för- och efternamn och får inte innehålla siffror.");
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
                    Console.WriteLine("Ogiltig e-postadress. Försök igen.");
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
                    Console.WriteLine("Ogiltigt telefonnummer. Ange endast siffror (7-15 tecken).");
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
            Console.Write("Ange nytt namn (tryck Enter för att behålla): ");
            var newName = Console.ReadLine();
            customer.Name = string.IsNullOrWhiteSpace(newName) ? customer.Name : newName;

            Console.WriteLine($"Nuvarande e-post: {customer.Email}");
            Console.Write("Ange ny e-post (tryck Enter för att behålla): ");
            var newEmail = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newEmail) && Regex.IsMatch(newEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                customer.Email = newEmail;
            }
            else if (!string.IsNullOrWhiteSpace(newEmail))
            {
                Console.WriteLine("Ogiltig e-postadress. Ingen ändring gjord.");
            }

            Console.WriteLine($"Nuvarande telefonnummer: {customer.Phone}");
            Console.Write("Ange nytt telefonnummer (tryck Enter för att behålla): ");
            var newPhone = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newPhone) && Regex.IsMatch(newPhone, @"^\d{7,15}$"))
            {
                customer.Phone = newPhone;
            }
            else if (!string.IsNullOrWhiteSpace(newPhone))
            {
                Console.WriteLine("Ogiltigt telefonnummer. Ingen ändring gjord.");
            }

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
                Console.WriteLine("Felaktigt ID. Ange ett giltigt numeriskt kund-ID.");
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
                Console.WriteLine($"Är du säker på att du vill ta bort kunden: {customer.Name}? (ja/nej)");
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
