using DogsHouseService.SharedKernel.Results;

namespace DogsHouseService.Domain.Dogs.DogColors
{
    public static class DogColorErrors
    {
        public static readonly Error EmptyValue =
            Error.Validation(
                code: "DogColor.EmptyValue",
                description: "Color cannot be empty.");

        public static Error TooLong(int maxLength) =>
            Error.Validation(
                code: "DogColor.TooLong",
                description: $"Color length must not exceed {maxLength} characters.");
    }
}
