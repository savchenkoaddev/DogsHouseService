using DogsHouseService.Domain.Dogs.DogWeights;
using FluentAssertions;

namespace DogsHouseService.Domain.UnitTests.Dogs
{
    public sealed class DogWeightTests
    {
        [Theory]
        [InlineData(DogWeight.MinValue - 1)]
        [InlineData(DogWeight.MinValue - 100)]
        public void Create_IfValueTooSmall_ShouldReturnTooSmallError(int value)
        {
            // Act
            var result = DogWeight.Create(value);

            // Assert
            result.Error.Should().Be(DogWeightErrors.TooSmall(DogWeight.MinValue));
        }

        [Theory]
        [InlineData(DogWeight.MaxValue + 1)]
        [InlineData(DogWeight.MaxValue + 100)]
        public void Create_IfValueTooBig_ShouldReturnTooBigError(int value)
        {
            // Act
            var result = DogWeight.Create(value);

            // Assert
            result.Error.Should().Be(DogWeightErrors.TooBig(DogWeight.MaxValue));
        }

        [Theory]
        [InlineData(DogWeight.MinValue)]
        [InlineData(DogWeight.MaxValue - 1)]
        [InlineData(DogWeight.MaxValue)]
        public void Create_IfValidValue_ShouldReturnValidDogWeight(int value)
        {
            // Act
            var result = DogWeight.Create(value).Value;

            // Assert
            result.Value.Should().Be(value);
        }
    }
}
