using DogsHouseService.Domain.Dogs;
using DogsHouseService.Domain.Dogs.DogNames;
using Microsoft.EntityFrameworkCore;

namespace DogsHouseService.Infrastructure.Persistence.Repositories
{
    internal sealed class DogRepository : IDogRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DogRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsWithNameAsync(
            DogName name, 
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Dogs
                .AnyAsync(dog => dog.Name == name, cancellationToken);
        }

        public async Task InsertAsync(
            Dog dog, 
            CancellationToken cancellationToken = default)
        {
            await _dbContext.Dogs.AddAsync(
                entity: dog,
                cancellationToken);
        }
    }
}