using DogsHouseService.Application.Extensions.DI;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddMappersFromAssembly(typeof(DependencyInjection).Assembly);

            return services;
        }
    }
}
