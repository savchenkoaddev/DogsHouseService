using Microsoft.Extensions.DependencyInjection;
using DogsHouseService.Application.Extensions.DI;

namespace DogsHouseService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddMediatR();

            services.AddPipelineBehaviors();

            services.AddValidators();

            return services;
        }
    }
}
