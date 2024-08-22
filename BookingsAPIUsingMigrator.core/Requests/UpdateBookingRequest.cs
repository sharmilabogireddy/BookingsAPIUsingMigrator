using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsAPIUsingMigrator.core.Requests
{
    public class UpdateBookingRequest : CreateBookingRequest
    {
        public int Id { get; set; }
    }
}
