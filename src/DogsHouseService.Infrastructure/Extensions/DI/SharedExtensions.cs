using DogsHouseService.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DogsHouseService.Infrastructure.Extensions.DI
{
    public static class SharedExtensions
    {
        public static IServiceCollection ConfigureValidatableOnStartOptions<TOptions>(
           this IServiceCollection services,
           string? configSectionPath = default)
           where TOptions : class, IAppOptions
        {
            var sectionPath = configSectionPath ?? TOptions.SectionName;

            services.AddOptions<TOptions>()
                .BindConfiguration(sectionPath)
                .ValidateDataAnnotations()
                .ValidateOnStart();

            return services;
        }

        public static TOptions GetOptions<TOptions>(this IServiceProvider serviceProvider)
            where TOptions : class
        {
            return serviceProvider.GetRequiredService<IOptions<TOptions>>().Value;
        }
    }
}
