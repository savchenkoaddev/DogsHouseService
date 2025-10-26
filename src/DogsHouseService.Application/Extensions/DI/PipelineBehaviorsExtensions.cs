using DogsHouseService.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DogsHouseService.Application.Extensions.DI
{
    internal static class PipelineBehaviorsExtensions
    {
        public static IServiceCollection AddPipelineBehaviors(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

            return services;
        }
    }
}
