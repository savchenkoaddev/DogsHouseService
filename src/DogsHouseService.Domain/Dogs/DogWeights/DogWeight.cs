using DogsHouseService.SharedKernel.Primitives;
using DogsHouseService.SharedKernel.Results;

namespace DogsHouseService.Domain.Dogs.DogWeights
{
    public sealed class DogWeight : ValueObject
    {
        public const int MinValue = 1;
        public const int MaxValue = 200;

        private DogWeight(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public static Result<DogWeight> Create(int value)
        {
            if (value < MinValue)
            {
                return Result.Failure<DogWeight>(
                    DogWeightErrors.TooSmall(minValue: MinValue));
            }

            if (value > MaxValue)
            {
                return Result.Failure<DogWeight>(
                    DogWeightErrors.TooBig(maxValue: MaxValue));
            }

            return new DogWeight(value);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return MinValue;
            yield return MaxValue;
        }
    }
}
