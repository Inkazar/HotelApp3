using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp3.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; } // FK till Customer
        public int RoomId { get; set; } // FK till Room
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ExtraBeds { get; set; } // Extra sängar i bokningen
        public Customer Customer { get; set; } // Navigation till Customer
        public Room Room { get; set; } // Navigation till Room
    }

}
