using DogsHouseService.SharedKernel.Primitives;
using DogsHouseService.SharedKernel.Results;

namespace DogsHouseService.Domain.Dogs.TailLengths
{
    public sealed class TailLength : ValueObject
    {
        public const int MinLength = 0;
        public const int MaxLength = 200;

        private TailLength(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public static Result<TailLength> Create(int value)
        {
            if (value < MinLength)
            {
                return Result.Failure<TailLength>(
                    TailLengthErrors.TooSmall(minValue: MinLength));
            }

            if (value > MaxLength)
            {
                return Result.Failure<TailLength>(
                    TailLengthErrors.TooBig(maxValue: MinLength));
            }

            return new TailLength(value);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return MinLength;
            yield return MaxLength;
        }
    }
}
