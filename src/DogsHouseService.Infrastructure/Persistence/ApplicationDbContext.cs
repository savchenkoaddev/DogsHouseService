using DogsHouseService.Domain.Dogs;
using Microsoft.EntityFrameworkCore;

namespace DogsHouseService.Infrastructure.Persistence
{
    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Dog> Dogs { get; init; }
    }
}
