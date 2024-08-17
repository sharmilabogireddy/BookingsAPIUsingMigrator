using BookingsAPIUsingMigrator.dataaccess.Data.Contexts;
using BookingsAPIUsingMigrator.migrator.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsAPIUsingMigrator.migrator
{
    public class DbContextFactory : IDesignTimeDbContextFactory<BookingRepositoryContext>
    {
        public BookingRepositoryContext CreateDbContext(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", optional: false)
                      .AddJsonFile($"appsettings.{env}.json", true, true)
                      .AddJsonFile($"/etc/appsettings/appsettings.{env}.json", true, true);

            IConfiguration config = builder.Build();

            var dbConfig = config.GetSection("DbSettings").Get<DbSettings>();

            var bookingDbBuilder = new DbContextOptionsBuilder<BookingRepositoryContext>()
                    .UseNpgsql($"Host={dbConfig!.Host};Database={dbConfig.Database};Username={dbConfig.Username};Password={dbConfig.Password}",
                            b => b.MigrationsAssembly("BookingsAPIUsingMigrator.migrator"));

            return new BookingRepositoryContext(bookingDbBuilder.Options);
        }
    }
}
