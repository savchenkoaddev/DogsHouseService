using DogsHouseService.Application.Abstractions.Data;
using DogsHouseService.Application.UseCases.Dogs.Commands.CreateDog;
using DogsHouseService.Domain.Dogs;
using DogsHouseService.Domain.Dogs.DogColors;
using DogsHouseService.Domain.Dogs.DogNames;
using DogsHouseService.Domain.Dogs.DogWeights;
using DogsHouseService.Domain.Dogs.TailLengths;
using DogsHouseService.SharedKernel.Results;

namespace DogsHouseService.Application.UseCases.Dogs.Mappers
{
    internal sealed class CreateDogCommandToEntityMapper
        : Mapper<CreateDogCommand, Result<Dog>>
    {
        public override Result<Dog> Map(CreateDogCommand source)
        {
            var createNameResult = DogName.Create(source.Name!);

            if (createNameResult.IsFailure)
            {
                return Result.Failure<Dog>(createNameResult.Error);
            }

            var createColorResult = DogColor.Create(source.Color!);

            if (createColorResult.IsFailure)
            {
                return Result.Failure<Dog>(createColorResult.Error);
            }

            var createTailLengthResult = TailLength.Create(source.TailLength!.Value);

            if (createTailLengthResult.IsFailure)
            {
                return Result.Failure<Dog>(createTailLengthResult.Error);
            }

            var createWeightResult = DogWeight.Create(source.Weight!.Value);

            if (createWeightResult.IsFailure)
            {
                return Result.Failure<Dog>(createWeightResult.Error);
            }

            var id = new DogId(Guid.NewGuid());

            var result = Dog.Create(
                id,
                name: createNameResult.Value,
                color: createColorResult.Value,
                tailLength: createTailLengthResult.Value,
                weight: createWeightResult.Value);

            if (result.IsFailure)
            {
                return Result.Failure<Dog>(result.Error);
            }

            return result.Value;
        }
    }
}
