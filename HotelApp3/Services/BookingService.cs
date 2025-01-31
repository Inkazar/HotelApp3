using HotelApp3.Models;
using HotelApp3.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp3.Services
{
    public class BookingService
    {
        private readonly BookingRepository _repository;

        public BookingService(BookingRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return _repository.GetAllBookings().ToList();
        }

        public void AddBooking(Booking booking)
        {
            _repository.AddBooking(booking);
        }
        public void UpdateBooking(Booking booking)
        {
            _repository.UpdateBooking(booking);
        }
        public void DeleteBooking(int bookingId)
        {
            _repository.DeleteBooking(bookingId);
        }
        public Booking GetBookingById(int bookingId)
        {
            return _repository.GetById(bookingId);
        }

        
    }
}
