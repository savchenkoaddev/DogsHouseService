using DogsHouseService.Domain.Dogs.DogNames;
using DogsHouseService.SharedKernel.Results;

namespace DogsHouseService.Domain.Dogs
{
    public static class DogErrors
    {
        public static Error DuplicateName(DogName name) =>
            Error.Validation(
                code: "Dog.DuplicateName",
                description: $"A dog with the name '{name.Value}' already exists.");
    }
}
