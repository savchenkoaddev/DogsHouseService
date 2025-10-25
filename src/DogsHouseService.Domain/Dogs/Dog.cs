using DogsHouseService.Domain.Dogs.DogNames;
using DogsHouseService.Domain.Dogs.DogWeights;
using DogsHouseService.Domain.Dogs.TailLengths;
using DogsHouseService.SharedKernel.Primitives;
using DogsHouseService.SharedKernel.Results;

namespace DogsHouseService.Domain.Dogs
{
    public sealed class Dog : AggregateRoot<DogId>
    {
        private DogName _dogName;
        private TailLength _tailLength;
        private DogWeight _weight;

        private Dog()
            : base(new(Guid.Empty))
        { }

        private Dog(
            DogId id,
            DogName name,
            TailLength tailLength,
            DogWeight weight) : base(id)
        {
            Name = name;
            TailLength = tailLength;
            Weight = weight;
        }

        public DogName Name
        {
            get
            {
                return _dogName;
            }
            private set
            {
                ArgumentNullException.ThrowIfNull(value, nameof(value));

                _dogName = value;
            }
        }

        public TailLength TailLength
        {
            get
            {
                return _tailLength;
            }
            private set
            {
                ArgumentNullException.ThrowIfNull(value, nameof(value));

                _tailLength = value;
            }
        }

        public DogWeight Weight
        {
            get
            {
                return _weight;
            }
            private set
            {
                ArgumentNullException.ThrowIfNull(value, nameof(value));

                _weight = value;
            }
        }

        public static Result<Dog> Create(
            DogId id,
            DogName name,
            TailLength tailLength,
            DogWeight weight)
        {
            return new Dog(id, name, tailLength, weight);
        }
    }
}
