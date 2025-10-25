using DogsHouseService.Application.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace DogsHouseService.Infrastructure.Persistence.Options
{
    internal sealed class DbSettings : IAppOptions
    {
        public static string SectionName => nameof(DbSettings);

        [Required]
        public required string ConnectionString { get; init; }
    }
}
