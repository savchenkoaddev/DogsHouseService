using DogsHouseService.SharedKernel.Results;
using MediatR;

namespace DogsHouseService.Application.UseCases.Dogs.Commands
{
    public sealed record CreateDogCommand(
        string? Name,
        string? Color,
        int? TailLength,
        int? Weight) : IRequest<Result<Guid>>;
}
