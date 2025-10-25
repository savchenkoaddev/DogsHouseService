using DogsHouseService.SharedKernel.Primitives;
using DogsHouseService.SharedKernel.Results;

namespace DogsHouseService.Domain.Dogs.DogNames
{
    public sealed class DogName : ValueObject
    {
        public const int MaxLength = 100;

        private DogName(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<DogName> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result.Failure<DogName>(
                    DogNameErrors.EmptyValue);
            }

            if (value.Length > MaxLength)
            {
                return Result.Failure<DogName>(
                    DogNameErrors.TooLong(maxLength: MaxLength));
            }

            return new DogName(value);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
