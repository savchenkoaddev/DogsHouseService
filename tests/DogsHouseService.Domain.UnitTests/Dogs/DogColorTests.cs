using DogsHouseService.Domain.Dogs.DogColors;
using FluentAssertions;

namespace DogsHouseService.Domain.UnitTests.Dogs
{
    public sealed class DogColorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_IfEmptyValue_ShouldReturnEmptyValueError(string value)
        {
            // Act
            var result = DogColor.Create(value);

            // Assert
            result.Error.Should().Be(DogColorErrors.EmptyValue);
        }

        [Fact]
        public void Create_IfLengthExceededMaxLength_ShouldReturnTooLongError()
        {
            // Arrange
            var length = DogColor.MaxLength + 1;
            var value = new string('R', length);

            // Act
            var result = DogColor.Create(value);

            // Assert
            result.Error.Should().Be(DogColorErrors.TooLong(DogColor.MaxLength));
        }

        [Fact]
        public void Create_IfValidValue_ShouldReturnValidDogColor()
        {
            // Arrange
            var color = "Brown&red";

            // Act
            var result = DogColor.Create(color).Value;

            // Assert
            result.Value.Should().Be(color);
        }
    }
}
