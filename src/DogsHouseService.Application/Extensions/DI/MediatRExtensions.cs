using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DogsHouseService.Application.Extensions.DI
{
    internal static class MediatRExtensions
    {
        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
