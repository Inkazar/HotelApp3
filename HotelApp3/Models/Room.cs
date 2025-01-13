using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp3.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int MaxCapacity { get; set; }
        public decimal PricePerNight { get; set; }
        public int ExtraBeds { get; set; } 
    }
}
