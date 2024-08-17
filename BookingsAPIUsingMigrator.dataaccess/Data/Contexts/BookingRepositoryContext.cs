using BookingsAPIUsingMigrator.dataaccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsAPIUsingMigrator.dataaccess.Data.Contexts
{
    public class BookingRepositoryContext : DbContext
    {
        public BookingRepositoryContext(DbContextOptions options)
    :   base(options)
        {
        }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>();
        }
    }
}
