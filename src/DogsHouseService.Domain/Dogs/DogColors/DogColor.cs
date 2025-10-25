using DogsHouseService.SharedKernel.Primitives;
using DogsHouseService.SharedKernel.Results;

namespace DogsHouseService.Domain.Dogs.DogColors
{
    public sealed class DogColor : ValueObject
    {
        public const int MaxLength = 100;

        private DogColor(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<DogColor> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result.Failure<DogColor>(
                    DogColorErrors.EmptyValue);
            }

            if (value.Length > MaxLength)
            {
                return Result.Failure<DogColor>(
                    DogColorErrors.TooLong(maxLength: MaxLength));
            }

            return new DogColor(value);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
