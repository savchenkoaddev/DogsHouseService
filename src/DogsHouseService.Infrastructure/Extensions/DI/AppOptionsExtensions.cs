using DogsHouseService.Infrastructure.Persistence.Options;
using Microsoft.Extensions.DependencyInjection;

namespace DogsHouseService.Infrastructure.Extensions.DI
{
    internal static class AppOptionsExtensions
    {
        public static IServiceCollection AddAppOptions(
           this IServiceCollection services)
        {
            services.ConfigureValidatableOnStartOptions<DbSettings>();

            return services;
        }
    }
}
