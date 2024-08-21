using BookingsAPIUsingMigrator.dataaccess.Entities;
using BookingsAPIUsingMigrator.dataaccess.Repositories.Interfaces;

namespace BookingsAPIUsingMigrator.dataaccess
{
    public interface IBookingRepositoryManager : IDisposable
    {
        IBookingRepository BookingRepository { get; }
        Task Save();
    }
}
