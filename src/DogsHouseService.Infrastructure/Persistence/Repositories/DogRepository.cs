using DogsHouseService.Domain.Dogs;
using DogsHouseService.Domain.Dogs.DogNames;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public async Task<List<Dog>> GetAllAsync(
            string? sortColumn,
            bool sortAscending,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Dog> dogsQuery = _dbContext.Dogs;

            Expression<Func<Dog, object>> keySelector = GetSortProperty(sortColumn);

            if (sortAscending)
            {
                dogsQuery = dogsQuery.OrderBy(keySelector);
            }
            else
            {
                dogsQuery = dogsQuery.OrderByDescending(keySelector);
            }

            var dogs = await dogsQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return dogs;
        }

        private static Expression<Func<Dog, object>> GetSortProperty(string? sortColumn)
        {
            return sortColumn?.ToLower() switch
            {
                "name" => dog => dog.Name,
                "color" => dog => dog.Color,
                "taillength" => dog => dog.TailLength,
                "tail-length" => dog => dog.TailLength,
                "tail_length" => dog => dog.TailLength,
                "weight" => dog => dog.Weight,
                _ => dog => dog.Id
            };
        }
    }
}