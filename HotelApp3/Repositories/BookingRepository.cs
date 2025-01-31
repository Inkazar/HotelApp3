using HotelApp3.Models;
using HotelApp3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp3.Repositories
{
    public class BookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Booking> GetAllBookings()
        {
            return _context.Bookings;
        }

        public void AddBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }
        public void UpdateBooking(Booking booking)
        {
            var existingBooking = _context.Bookings.Find(booking.BookingId);
            if (existingBooking != null)
            {
                existingBooking.StartDate = booking.StartDate;
                existingBooking.EndDate = booking.EndDate;
                existingBooking.ExtraBeds = booking.ExtraBeds;
                _context.SaveChanges();
            }
        }

        public Booking GetById(int bookingId)
        {
            return _context.Bookings.Find(bookingId);
        }

        public IEnumerable<Booking> GetAll()
        {
            return _context.Bookings.ToList();
        }

        public void DeleteBooking(int bookingId)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                _context.SaveChanges();
            }
        }
    }
}
