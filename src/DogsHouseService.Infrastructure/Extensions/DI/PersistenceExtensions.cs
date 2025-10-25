using DogsHouseService.Infrastructure.Persistence;
using DogsHouseService.Infrastructure.Persistence.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DogsHouseService.Infrastructure.Extensions.DI
{
    internal static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services)
        {
            services.AddAppDbContext();

            return services;
        }

        private static IServiceCollection AddAppDbContext(
            this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                (serviceProvider, options) =>
                {
                    var dbSettings = serviceProvider.GetOptions<DbSettings>();

                    options.UseSqlServer(
                        connectionString: dbSettings.ConnectionString);
                });

            return services;
        }
    }
}
