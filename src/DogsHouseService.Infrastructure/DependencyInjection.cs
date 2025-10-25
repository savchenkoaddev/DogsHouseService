using DogsHouseService.Infrastructure.Extensions.DI;
using Microsoft.Extensions.DependencyInjection;

namespace DogsHouseService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services)
        {
            services.AddAppOptions();

            services.AddPersistence();

            return services;
        }
    }
}
