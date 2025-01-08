using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp3.Controllers
{
    public class BookingController
    {
        private readonly BookingHandler _handler;

        public BookingController(BookingHandler handler)
        {
            _handler = handler;
        }

        public void AddBooking() => _handler.AddBooking();

        public void DeleteBooking() => _handler.DeleteBooking();

        public void ViewBookings() => _handler.ViewBookings();
    }
}
