using DogsHouseService.Domain.Dogs;
using DogsHouseService.Domain.Dogs.DogColors;
using DogsHouseService.Domain.Dogs.DogNames;
using DogsHouseService.Domain.Dogs.DogWeights;
using DogsHouseService.Domain.Dogs.TailLengths;

namespace DogsHouseService.Testing.Shared.Factories
{
    public static class DogFixtureFactory
    {
        public static DogName CreateName() => DogName.Create("Doggy").Value;

        public static DogColor CreateColor() => DogColor.Create("Red").Value;

        public static TailLength CreateTailLength() => TailLength.Create(1).Value;

        public static DogWeight CreateWeight() => DogWeight.Create(20).Value;

        public static Dog CreateDog()
        {
            return Dog.Create(
                id: new DogId(Guid.NewGuid()),
                name: CreateName(),
                color: CreateColor(),
                tailLength: CreateTailLength(),
                weight: CreateWeight()).Value;
        }
    }
}
