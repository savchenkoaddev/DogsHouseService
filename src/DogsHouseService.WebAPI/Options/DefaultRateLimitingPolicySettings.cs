using DogsHouseService.Application.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace DogsHouseService.WebAPI.Options
{
    internal sealed class DefaultRateLimitingPolicySettings : IAppOptions
    {
        public static string SectionName => nameof(DefaultRateLimitingPolicySettings);

        [Required]
        [Range(1, 10000)]
        public int PermitLimit { get; init; }

        [Required]
        [Range(1, 10000)]
        public int WindowInSeconds { get; init; }
    }
}
