using DogsHouseService.Application.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace DogsHouseService.WebAPI.Options
{
    internal sealed class RateLimitingSettings : IAppOptions
    {
        public static string SectionName => nameof(RateLimitingSettings);

        [Required]
        [Range(StatusCodes.Status400BadRequest, StatusCodes.Status511NetworkAuthenticationRequired)]
        public int RejectionStatusCode { get; init; }
    }
}
