using BookingsAPIUsingMigrator.dataaccess.Data.Contexts;
using BookingsAPIUsingMigrator.dataaccess.Repositories;
using BookingsAPIUsingMigrator.dataaccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsAPIUsingMigrator.dataaccess
{
    public class BookingRepositoryManager : IBookingRepositoryManager
    {
        private readonly BookingRepositoryContext _bookingRepositoryContext;
        private IBookingRepository _bookingRepository;
        private bool disposed = false;

        public BookingRepositoryManager(BookingRepositoryContext bookingRepositoryContext)
        {
            _bookingRepositoryContext = bookingRepositoryContext;
        }

        public IBookingRepository BookingRepository
        {
            get
            {
                _bookingRepository ??= new BookingRepository(_bookingRepositoryContext);
                return _bookingRepository;
            }
        }

        public async Task Save() => await _bookingRepositoryContext.SaveChangesAsync();
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _bookingRepositoryContext.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
