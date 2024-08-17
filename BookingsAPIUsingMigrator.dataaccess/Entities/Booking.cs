using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsAPIUsingMigrator.dataaccess.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public string name { get; set; }

        public string? bookingTime { get; set; }
    }
}
