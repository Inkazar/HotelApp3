using System;
using HotelApp3.Models;
using HotelApp3.Utilities.Functions;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp3.Controllers
{
    public class BookingController
    {
        private readonly BookingFunction _handler;

        public BookingController(BookingFunction handler)
        {
            _handler = handler;
        }

        public void AddBooking() => _handler.AddBooking();

        public void DeleteBooking() => _handler.DeleteBooking();

        public void ViewBookings() => _handler.ViewBookings();
        
        public void BookingUpdate() => _handler.UpdateBooking();
    }
}
