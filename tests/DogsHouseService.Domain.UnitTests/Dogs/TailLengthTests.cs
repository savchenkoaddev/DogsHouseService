using DogsHouseService.Domain.Dogs.TailLengths;
using FluentAssertions;

namespace DogsHouseService.Domain.UnitTests.Dogs
{
    public sealed class TailLengthTests
    {
        [Theory]
        [InlineData(TailLength.MinLength - 1)]
        [InlineData(TailLength.MinLength - 100)]
        public void Create_IfValueTooSmall_ShouldReturnTooSmallError(int value)
        {
            // Act
            var result = TailLength.Create(value);

            // Assert
            result.Error.Should().Be(TailLengthErrors.TooSmall(TailLength.MinLength));
        }

        [Theory]
        [InlineData(TailLength.MaxLength + 1)]
        [InlineData(TailLength.MaxLength + 150)]
        public void Create_IfValueTooBig_ShouldReturnTooBigError(int value)
        {
            // Act
            var result = TailLength.Create(value);

            // Assert
            result.Error.Should().Be(TailLengthErrors.TooBig(TailLength.MaxLength));
        }

        [Theory]
        [InlineData(TailLength.MinLength)]
        [InlineData(TailLength.MaxLength - 1)]
        [InlineData(TailLength.MaxLength)]
        public void Create_IfValidValue_ShouldReturnValidTailLength(int value)
        {
            // Act
            var result = TailLength.Create(value).Value;

            // Assert
            result.Value.Should().Be(value);
        }
    }
}
