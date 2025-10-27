using DogsHouseService.Domain.Dogs.DogColors;
using DogsHouseService.Domain.Dogs.DogNames;
using DogsHouseService.SharedKernel.Results;
using FluentAssertions;

namespace DogsHouseService.Domain.UnitTests.Dogs
{
    public sealed class DogNameTests
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
            var length = DogName.MaxLength + 1;
            var value = new string('A', length);

            // Act
            var result = DogName.Create(value);

            // Assert
            result.Error.Should().Be(DogNameErrors.TooLong(DogName.MaxLength));
        }

        [Fact]
        public void Create_IfValidValue_ShouldReturnValidDogName()
        {
            // Arrange
            var name = "John";

            // Act
            var result = DogName.Create(name).Value;

            // Assert
            result.Value.Should().Be(name);
        }
    }
}
