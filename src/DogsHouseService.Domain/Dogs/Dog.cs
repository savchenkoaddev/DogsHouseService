using DogsHouseService.Domain.Dogs.DogColors;
using DogsHouseService.Domain.Dogs.DogNames;
using DogsHouseService.Domain.Dogs.DogWeights;
using DogsHouseService.Domain.Dogs.TailLengths;
using DogsHouseService.SharedKernel.Primitives;
using DogsHouseService.SharedKernel.Results;

namespace DogsHouseService.Domain.Dogs
{
    public sealed class Dog : AggregateRoot<DogId>
    {
        private DogName _name;
        private DogColor _color;
        private TailLength _tailLength;
        private DogWeight _weight;

        private Dog()
            : base(new(Guid.Empty))
        { }

        private Dog(
            DogId id,
            DogName name,
            DogColor color,
            TailLength tailLength,
            DogWeight weight) : base(id)
        {
            Name = name;
            Color = color;
            TailLength = tailLength;
            Weight = weight;
        }

        public DogName Name
        {
            get
            {
                return _name;
            }
            private set
            {
                ArgumentNullException.ThrowIfNull(value, nameof(value));

                _name = value;
            }
        }

        public DogColor Color
        {
            get
            {
                return _color;
            }
            private set
            {
                ArgumentNullException.ThrowIfNull(value, nameof(value));

                _color = value;
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
            DogColor color,
            TailLength tailLength,
            DogWeight weight)
        {
            return new Dog(id, name, color, tailLength, weight);
        }
    }
}
