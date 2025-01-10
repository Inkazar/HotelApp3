using HotelApp3.Models;
using HotelApp3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp3.Utilities.Functions
{
    public class BookingFunction
    {
        private readonly BookingService _bookingService;
        private readonly CustomerService _customerService;
        private readonly RoomService _roomService;
        

        public BookingFunction(BookingService bookingService, CustomerService customerService,
            RoomService roomService)
        {
            _bookingService = bookingService;
            _customerService = customerService;
            _roomService = roomService;
            
        }

        public void AddBooking()
        {
            Console.Clear();
            Console.WriteLine("--- Lägg till bokning ---");

            int roomId;
            do
            {
                Console.WriteLine("Tillgängliga Rum:");
                var rooms = _roomService.GetAllRooms().ToList();
                foreach (var room in rooms)
                {
                    Console.WriteLine($"ID: {room.RoomId}, Typ: {room.Type}, Kapacitet: {room.MaxCapacity}, Pris/Natt: {room.PricePerNight:C}, Extrasängar: {room.ExtraBeds}");
                }

                Console.Write("Ange rummets ID: ");
            } while (!int.TryParse(Console.ReadLine(), out roomId));

            var selectedRoom = _roomService.GetRoomById(roomId);
            if (selectedRoom == null)
            {
                Console.WriteLine("Ogiltigt rum. Försök igen.");
                Console.ReadKey();
                return;
            }

            DateTime startDate;
            do
            {
                Console.Write("Ange startdatum (yyyy-MM-dd): ");
            } while (!DateTime.TryParse(Console.ReadLine(), out startDate));

            DateTime endDate;
            do
            {
                Console.Write("Ange slutdatum (yyyy-MM-dd): ");
            } while (!DateTime.TryParse(Console.ReadLine(), out endDate) || endDate <= startDate);

            int extraBeds = 0;
            if (selectedRoom.MaxCapacity > 2)
            {
                Console.WriteLine("Det här rummet kan ha extrasängar.");
                do
                {
                    Console.Write($"Hur många extrasängar vill du lägga till? (Max: {selectedRoom.MaxCapacity - 2}): ");
                } while (!int.TryParse(Console.ReadLine(), out extraBeds) || extraBeds < 0 || extraBeds > (selectedRoom.MaxCapacity - 2));
            }

            string addCustomerChoice;
            do
            {
                Console.Write("Vill du lägga till en ny kund? (ja/nej): ");
                addCustomerChoice = Console.ReadLine()?.ToLower();
            } while (addCustomerChoice != "ja" && addCustomerChoice != "nej");

            int customerId;
            if (addCustomerChoice == "ja")
            {
                Console.Write("Ange kundens namn: ");
                var name = Console.ReadLine();
                Console.Write("Ange kundens e-post: ");
                var email = Console.ReadLine();
                Console.Write("Ange kundens telefonnummer: ");
                var phone = Console.ReadLine();

                var newCustomer = new Customer { Name = name, Email = email, Phone = phone };
                _customerService.AddCustomer(newCustomer);
                customerId = newCustomer.CustomerId;
            }
            else
            {
                do
                {
                    Console.WriteLine("Befintliga kunder:");
                    var customers = _customerService.GetAllCustomers().ToList();
                    foreach (var customer in customers)
                    {
                        Console.WriteLine($"ID: {customer.CustomerId}, Namn: {customer.Name}, E-post: {customer.Email}, Telefon: {customer.Phone}");
                    }
                    Console.Write("Ange befintligt kund-ID: ");
                } while (!int.TryParse(Console.ReadLine(), out customerId) || !_customerService.GetAllCustomers().Any(c => c.CustomerId == customerId));
            }

            var booking = new Booking
            {
                CustomerId = customerId,
                RoomId = roomId,
                StartDate = startDate,
                EndDate = endDate,
                ExtraBeds = extraBeds
            };

            _bookingService.AddBooking(booking);
            Console.WriteLine("Bokningen har lagts till!");
            Console.ReadKey();
        }

        public void DeleteBooking()
        {
            Console.Clear();
            Console.WriteLine("--- Ta bort bokning ---");
            ViewBookings();

            int bookingId;
            do
            {
                Console.Write("Ange boknings-ID att ta bort: ");
            } while (!int.TryParse(Console.ReadLine(), out bookingId));

            _bookingService.DeleteBooking(bookingId);
            Console.WriteLine("Bokningen har tagits bort!");
            Console.ReadKey();
        }

        public void ViewBookings()
        {
            Console.Clear();
            Console.WriteLine("--- Alla Bokningar ---");

            var bookings = _bookingService.GetAllBookings().ToList();
            foreach (var booking in bookings)
            {
                Console.WriteLine($"Bokning-ID: {booking.BookingId}, Kund-ID: {booking.CustomerId}, Rum-ID: {booking.RoomId}, Startdatum: {booking.StartDate:yyyy-MM-dd}, Slutdatum: {booking.EndDate:yyyy-MM-dd}, Extrasängar: {booking.ExtraBeds}");
            }
            Console.WriteLine("Tryck på valfri tangent för att återgå.");
            Console.ReadKey();
        }
    }
}
