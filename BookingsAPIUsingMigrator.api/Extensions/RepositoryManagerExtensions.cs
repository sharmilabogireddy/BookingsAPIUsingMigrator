using BookingsAPIUsingMigrator.dataaccess;
using BookingsAPIUsingMigrator.dataaccess.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookingsAPIUsingMigrator.api.Extensions
{
    public static class RepositoryManagerExtensions
    {
        public static IServiceCollection ConfigureRepositoryManager(this IServiceCollection services, IConfiguration configuration)
        {
            //  Create the repository manager registration in the DI.
            services.AddDbContext<BookingRepositoryContext>(options =>
                    options.UseNpgsql(configuration.GetValue<string>("Database:ConnectionString")));

            services.AddScoped<IBookingRepositoryManager, BookingRepositoryManager>();

            return services;
        }
    }
}
