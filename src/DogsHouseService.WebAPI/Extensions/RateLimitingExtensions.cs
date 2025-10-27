using DogsHouseService.WebAPI.Options;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;
using System.Threading.RateLimiting;

namespace DogsHouseService.WebAPI.Extensions
{
    public static class RateLimitingExtensions
    {
        public const string DefaultPolicyName = "fixed-default";

        public static IServiceCollection AddRateLimiting(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var rateLimitingSettings = GetRateLimitingSettings(configuration);
            var defaultPolicySettings = GetDefaultLimitingPolicySettings(configuration);

            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = rateLimitingSettings?.RejectionStatusCode
                    ?? StatusCodes.Status429TooManyRequests;

                AddDefaultPolicy(options, defaultPolicySettings);
            });

            return services;
        }

        private static RateLimitingSettings? GetRateLimitingSettings(
            IConfiguration configuration)
        {
            return configuration.GetRequiredSection(
                RateLimitingSettings.SectionName)
                    .Get<RateLimitingSettings>();
        }

        private static DefaultRateLimitingPolicySettings? GetDefaultLimitingPolicySettings(
            IConfiguration configuration)
        {
            return configuration.GetRequiredSection(
                DefaultRateLimitingPolicySettings.SectionName)
                    .Get<DefaultRateLimitingPolicySettings>();
        }

        private static void AddDefaultPolicy(
            RateLimiterOptions options,
            DefaultRateLimitingPolicySettings? settings)
        {
            options.AddPolicy(DefaultPolicyName, context =>
            {
                return RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = settings?.PermitLimit ?? 100,
                        Window = TimeSpan.FromSeconds(settings?.WindowInSeconds ?? 30),
                    });
            });
        }
    }
}
