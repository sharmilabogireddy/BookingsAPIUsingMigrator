using BookingsAPIUsingMigrator.dataaccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsAPIUsingMigrator.test.Common
{
    public static class StoreFactory
    {
        public static List<Booking> BookingStore = new()
        {
            new Booking
            {
                Id = 1,
                name = "Test1",
                bookingTime = "09:00"
            },
            new Booking
            {
                Id = 2,
                name = "Test2",
                bookingTime = "09:00"
            },
            new Booking
            {
                Id = 3,
                name = "Test3",
                bookingTime = "09:00"
            },
            new Booking
            {
                Id = 4,
                name = "Test4",
                bookingTime = "09:00"
            },
            new Booking
            {
                Id = 5,
                name = "Test5",
                bookingTime = "10:00"
            },
        };
    }
}
