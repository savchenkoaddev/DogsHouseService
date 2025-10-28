using DogsHouseService.Application.UseCases.Dogs.Commands.CreateDog;

namespace DogsHouseService.Testing.Shared.Factories
{
    public static class CommandFixtureFactory
    {
        public static CreateDogCommand BuildCreateDogCommand(
            string? name = "Doggy",
            string? color = "Red",
            int tailLength = 1,
            int weight = 20)
        {
            return new CreateDogCommand(
                Name: name!,
                Color: color!,
                TailLength: tailLength,
                Weight: weight);
        }
    }
}
