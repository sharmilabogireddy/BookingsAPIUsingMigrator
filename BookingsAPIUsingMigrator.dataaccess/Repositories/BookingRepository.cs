using BookingsAPIUsingMigrator.dataaccess.Data.Contexts;
using BookingsAPIUsingMigrator.dataaccess.Entities;
using BookingsAPIUsingMigrator.dataaccess.Repositories.Interfaces;
using BookingsAPIUsingMigrator.shared.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsAPIUsingMigrator.dataaccess.Repositories
{
    public class BookingRepository : RepositoryBase<Booking>, IBookingRepository
    {
        private new readonly BookingRepositoryContext _repositoryContext;

        public BookingRepository(BookingRepositoryContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public async Task<Booking?> FindById(int id) => _repositoryContext.Bookings.FirstOrDefault(i => i.Id == id);

        public async Task<int> GetCountByBookingTime(string bookingTime)
        {
            return _repositoryContext.Bookings.Count(e => e.bookingTime == bookingTime);
        }

    }
}
