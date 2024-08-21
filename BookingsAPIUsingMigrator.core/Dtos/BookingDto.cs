using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsAPIUsingMigrator.core.Dtos
{
    public class BookingDto
    {
        public int id { get; set; }
        public required string name { get; set; }

        public string? bookingTime { get; set; }
    }
}
