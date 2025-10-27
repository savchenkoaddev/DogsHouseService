using DogsHouseService.Application.Abstractions.Data;
using DogsHouseService.Application.UseCases.Dogs.DTO;
using DogsHouseService.Domain.Dogs;
using DogsHouseService.SharedKernel.Results;
using MediatR;

namespace DogsHouseService.Application.UseCases.Dogs.Queries.GetDogs
{
    internal sealed class GetDogsQueryHandler
        : IRequestHandler<GetDogsQuery, Result<List<DogResponse>>>
    {
        private readonly IDogRepository _dogRepository;
        private readonly Mapper<Dog, DogResponse> _entityMapper;

        public GetDogsQueryHandler(
            IDogRepository dogRepository, 
            Mapper<Dog, DogResponse> entityMapper)
        {
            _dogRepository = dogRepository;
            _entityMapper = entityMapper;
        }

        public async Task<Result<List<DogResponse>>> Handle(
            GetDogsQuery request,
            CancellationToken cancellationToken)
        {
            bool sortAscending = request.Order?.ToLower() == "asc";

            var dogs = await _dogRepository.GetAllAsync(
                sortColumn: request.Attribute,
                sortAscending: sortAscending,
                page: request.PageNumber,
                pageSize: request.PageSize,
                cancellationToken);

            return Result.Success(_entityMapper.Map(dogs).ToList());
        }
    }
}
