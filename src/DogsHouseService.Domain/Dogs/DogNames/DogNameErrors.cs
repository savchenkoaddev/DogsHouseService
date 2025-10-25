using DogsHouseService.SharedKernel.Results;

namespace DogsHouseService.Domain.Dogs.DogNames
{
    public static class DogNameErrors
    {
        public static readonly Error EmptyValue =
            Error.Validation(
                code: "DogName.EmptyValue",
                description: "Name cannot be empty.");

        public static Error TooLong(int maxLength) =>
            Error.Validation(
                code: "DogName.TooLong",
                description: $"Name length must not exceed {maxLength} characters.");
    }
}
