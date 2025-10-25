using DogsHouseService.SharedKernel.Results;

namespace DogsHouseService.Domain.Dogs.TailLengths
{
    public static class TailLengthErrors
    {
        public static Error TooSmall(int minValue) =>
            Error.Validation(
                code: "TailLength.TooSmall",
                description: $"Tail length must be at least {minValue}.");

        public static Error TooBig(int maxValue) =>
            Error.Validation(
                code: "TailLength.TooBig",
                description: $"Tail length must not exceed {maxValue}.");
    }
}
