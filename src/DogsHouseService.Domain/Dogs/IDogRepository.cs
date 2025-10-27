using DogsHouseService.Domain.Dogs.DogNames;

namespace DogsHouseService.Domain.Dogs
{
    public interface IDogRepository
    {
        Task InsertAsync(
            Dog dog,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsWithNameAsync(
            DogName name,
            CancellationToken cancellationToken = default);

        Task<List<Dog>> GetAllAsync(
            string? sortColumn,
            bool sortAscending,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default);
    }
}
