using DogsHouseService.Application.Abstractions.Data;
using DogsHouseService.Domain.Dogs;
using DogsHouseService.Infrastructure.Persistence;
using DogsHouseService.Infrastructure.Persistence.Options;
using DogsHouseService.Infrastructure.Persistence.Repositories;
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

            services.AddUnitOfWork();

            services.AddRepositories();

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

        private static IServiceCollection AddUnitOfWork(
            this IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped<IDogRepository, DogRepository>();
        }
    }
}
