using DogsHouseService.Application.Abstractions.Data;
using DogsHouseService.Domain.Dogs;
using DogsHouseService.SharedKernel.Results;
using MediatR;

namespace DogsHouseService.Application.UseCases.Dogs.Commands
{
    internal sealed class CreateDogCommandHandler
        : IRequestHandler<CreateDogCommand, Result<Guid>>
    {
        private readonly IDogRepository _dogRepository;
        private readonly Mapper<CreateDogCommand, Result<Dog>> _requestMapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDogCommandHandler(
            IDogRepository dogRepository,
            Mapper<CreateDogCommand, Result<Dog>> requestMapper,
            IUnitOfWork unitOfWork)
        {
            _dogRepository = dogRepository;
            _requestMapper = requestMapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(
            CreateDogCommand request,
            CancellationToken cancellationToken)
        {
            var mappingResult = _requestMapper.Map(request);

            if (mappingResult.IsFailure)
            {
                return Result.Failure<Guid>(mappingResult.Error);
            }

            var dog = mappingResult.Value;

            var existsWithSameName = await _dogRepository.ExistsWithNameAsync(
                name: dog.Name,
                cancellationToken);

            if (existsWithSameName)
            {
                return Result.Failure<Guid>(DogErrors.DuplicateName(dog.Name));
            }

            await _dogRepository.InsertAsync(
                dog, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(dog.Id.Value);
        }
    }
}
