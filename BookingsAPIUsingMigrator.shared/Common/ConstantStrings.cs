using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsAPIUsingMigrator.shared.Common
{
    public static class ConstantStrings
    {
        public static readonly string BOOKING_TIME_INVALID = "Booking time is not valid.";
        public static readonly string NAME_INVALID = "Name is not valid.";
        public static readonly string BOOKING_TIME_NOT_IN_BUSSINESS_HOURS= "Bookings are full at the specified time.";
        public static readonly string BOOKING_ARE_FULL = "Bookings are full at the specified time.";
        public static readonly string INVALID_REQUEST_DATA = "The request data is not valid";
        public static readonly string NO_REQUESTED_RECORD = "The requested data does not exist";
    }
}
