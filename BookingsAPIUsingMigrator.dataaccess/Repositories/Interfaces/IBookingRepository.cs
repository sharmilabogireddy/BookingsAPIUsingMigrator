using BookingsAPIUsingMigrator.dataaccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsAPIUsingMigrator.dataaccess.Repositories.Interfaces
{
    public interface IBookingRepository : IRepositoryBase<Booking>
    {
        Task<Booking?> FindById(int id);
    }
}
