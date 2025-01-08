﻿using HotelApp3.Models;
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

        public void AddCustomer()
        {
            Console.Clear();
            Console.WriteLine("--- Lägg till kund ---");

            Console.Write("Ange namn: ");
            var name = Console.ReadLine();

            string email;
            do
            {
                Console.Write("Ange e-post: ");
                email = Console.ReadLine();
                if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    Console.WriteLine("Ogiltig e-postadress. Försök igen.");
                    email = null;
                }
            } while (email == null);

            string phone;
            do
            {
                Console.Write("Ange telefonnummer (endast siffror, 7-15 tecken): ");
                phone = Console.ReadLine();
                if (!Regex.IsMatch(phone, @"^\d{7,15}$"))
                {
                    Console.WriteLine("Ogiltigt telefonnummer. Försök igen.");
                    phone = null;
                }
            } while (phone == null);

            var newCustomer = new Customer { Name = name, Email = email, Phone = phone };
            _customerService.AddCustomer(newCustomer);
            Console.WriteLine("Kunden har lagts till!");
            Console.ReadKey();
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
            Console.WriteLine("Tryck på valfri tangent för att återgå.");
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

            int customerId;
            do
            {
                Console.Write("Ange ID för kunden att ta bort: ");
            } while (!int.TryParse(Console.ReadLine(), out customerId));

            _customerService.DeleteCustomer(customerId);
            Console.WriteLine("Kunden har tagits bort!");
            Console.ReadKey();
        }
    }
}
