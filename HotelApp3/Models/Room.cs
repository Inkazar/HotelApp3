using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp3.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string Type { get; set; }
        public int MaxCapacity { get; set; }
        public decimal PricePerNight { get; set; }
        public int ExtraBeds { get; set; } // Antal extra sängar
        public ICollection<Booking> Bookings { get; set; } // Relation till bokningar
    }

}
