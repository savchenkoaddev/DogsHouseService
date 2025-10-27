using DogsHouseService.Application.Abstractions.Data;
using DogsHouseService.Application.UseCases.Dogs.DTO;
using DogsHouseService.Domain.Dogs;

namespace DogsHouseService.Application.UseCases.Dogs.Mappers
{
    internal sealed class DogToDogResponseMapper
        : Mapper<Dog, DogResponse>
    {
        public override DogResponse Map(Dog source)
        {
            return new DogResponse(
                Name: source.Name.Value,
                Color: source.Color.Value,
                TailLength: source.TailLength.Value,
                Weight: source.Weight.Value);
        }
    }
}
