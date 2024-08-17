using BookingsAPIUsingMigrator.migrator.Config;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsAPIUsingMigrator.migrator
{
    public class Startup
    {
        public Startup()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", optional: false)
                      .AddJsonFile($"appsettings.{env}.json", true, true)
                      .AddJsonFile($"/etc/appsettings/appsettings.{env}.json", true, true);

            IConfiguration config = builder.Build();
            DbSettings = config.GetSection("DbSettings").Get<DbSettings>();
        }

        public DbSettings? DbSettings { get; private set; }
    }
}
