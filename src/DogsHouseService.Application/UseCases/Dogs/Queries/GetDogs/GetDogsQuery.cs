using DogsHouseService.Application.UseCases.Dogs.DTO;
using DogsHouseService.SharedKernel.Results;
using MediatR;

namespace DogsHouseService.Application.UseCases.Dogs.Queries.GetDogs
{
    public sealed record GetDogsQuery(
        string? Attribute,
        string? Order = "asc",
        int PageNumber = 1,
        int PageSize = 10) : IRequest<Result<List<DogResponse>>>;
}
