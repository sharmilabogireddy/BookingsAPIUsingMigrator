using Microsoft.EntityFrameworkCore;
using BookingsAPIUsingMigrator.migrator;
using BookingsAPIUsingMigrator.dataaccess.Data.Contexts;

var startup = new Startup();

var host = startup.DbSettings?.Host;
var username = startup.DbSettings?.Username;
var password = startup.DbSettings?.Password;
var database = startup.DbSettings?.Database;

var bookingDb = new DbContextOptionsBuilder<BookingRepositoryContext>()
    .UseNpgsql($"Host={host};Database={database};Username={username};Password={password}",
            b => b.MigrationsAssembly("BookingsAPIUsingMigrator.dataaccess"));

var options = bookingDb.Options;

using (var bookingDbContext = new BookingRepositoryContext(options))
{
    // Create design-time services
}

Console.ReadLine();