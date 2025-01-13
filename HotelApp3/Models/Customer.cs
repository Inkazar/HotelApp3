using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp3.Models
{
    public class Customer
    {
        
            public int CustomerId { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public ICollection<Booking> Bookings { get; set; } // Relation till bokningar
        

    }
}
