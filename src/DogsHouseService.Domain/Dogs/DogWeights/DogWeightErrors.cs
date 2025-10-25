using DogsHouseService.SharedKernel.Results;

namespace DogsHouseService.Domain.Dogs.DogWeights
{
    public static class DogWeightErrors
    {
        public static Error TooSmall(int minValue) =>
           Error.Validation(
               code: "DogWeight.TooSmall",
               description: $"Dog weight must be at least {minValue}.");

        public static Error TooBig(int maxValue) =>
            Error.Validation(
                code: "DogWeight.TooBig",
                description: $"Dog weight must not exceed {maxValue}.");
    }
}
