using DogsHouseService.Application.Abstractions.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DogsHouseService.Application.Extensions.DI
{
    internal static class MapperExtensions
    {
        public static IServiceCollection AddMappersFromAssembly(
            this IServiceCollection services,
            params Assembly[] assemblies)
        {
            services.Scan(scan =>
                scan.FromAssemblies(assemblies)
                    .AddClasses(classes => classes.AssignableTo(typeof(Mapper<,>)), publicOnly: false)
                    .As(type =>
                    {
                        var baseMapperType = type.BaseType;

                        return baseMapperType is not null && baseMapperType.IsGenericType &&
                               baseMapperType.GetGenericTypeDefinition() == typeof(Mapper<,>)
                            ? [baseMapperType]
                            : [];
                    })
                    .WithScopedLifetime());

            return services;
        }
    }
}