using DogsHouseService.Application.UseCases.Dogs.Queries.GetDogs;

namespace DogsHouseService.Testing.Shared.Factories
{
    public static class QueryFixtureFactory
    {
        public static GetDogsQuery BuildGetDogsQuery(
            string? attribute = "name",
            string? order = "asc",
            int pageNumber = 1,
            int pageSize = 10)
        {
            return new GetDogsQuery(
                Attribute: attribute!,
                Order: order,
                PageNumber: pageNumber,
                PageSize: pageSize);
        }
    }
}
