﻿using HotelApp3.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp3.Utilities
{
    public class BookingMenu
    {
        private readonly BookingController _controller;

        public BookingMenu(BookingController controller)
        {
            _controller = controller;
        }

        public void ShowMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("--- Bokningshantering ---");
                Console.WriteLine("1. Lägg till bokning");
                Console.WriteLine("2. Ta bort bokning");
                Console.WriteLine("3. Visa alla bokningar");
                Console.WriteLine("4. Uppdatera bokningar");
                Console.WriteLine("5. Gå tillbaka till huvudmenyn");
                Console.Write("Välj ett alternativ: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        _controller.AddBooking();
                        break;
                    case "2":
                        _controller.DeleteBooking();
                        break;
                    case "3":
                        _controller.ViewBookings();
                        Console.WriteLine("Tryck på valfri tangent för att återgå.");
                        Console.ReadKey();
                        break;
                    case "4":
                        _controller.BookingUpdate();
                        break;
                    case "5":
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
