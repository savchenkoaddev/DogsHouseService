using DogsHouseService.Domain.Dogs;
using DogsHouseService.Domain.Dogs.DogColors;
using DogsHouseService.Domain.Dogs.DogNames;
using DogsHouseService.Domain.Dogs.DogWeights;
using DogsHouseService.Domain.Dogs.TailLengths;
using FluentAssertions;

namespace DogsHouseService.Domain.UnitTests.Dogs
{
    public sealed class DogTests
    {
        [Fact]
        public void Create_IfAllValid_ShouldReturnDog()
        {
            // Arrange
            var id = new DogId(Guid.NewGuid());
            var name = DogName.Create("Buddy").Value;
            var color = DogColor.Create("Brown").Value;
            var tailLength = TailLength.Create(50).Value;
            var weight = DogWeight.Create(20).Value;

            // Act
            var result = Dog.Create(id, name, color, tailLength, weight);

            // Assert
            result.Value.Id.Should().Be(id);
            result.Value.Name.Should().Be(name);
            result.Value.Color.Should().Be(color);
            result.Value.TailLength.Should().Be(tailLength);
            result.Value.Weight.Should().Be(weight);
        }

        [Theory]
        [InlineData(null, "Brown", 50, 20)]
        [InlineData("Buddy", null, 50, 20)]
        [InlineData("Buddy", "Brown", null, 20)]
        [InlineData("Buddy", "Brown", 50, null)]
        public void Create_IfAnyValueIsNull_ShouldThrowArgumentNullException(
            string nameValue,
            string colorValue,
            int? tailLengthValue,
            int? weightValue)
        {
            // Arrange
            var id = new DogId(Guid.NewGuid());

            DogName? name = nameValue is null ? null : DogName.Create(nameValue).Value;
            DogColor? color = colorValue is null ? null : DogColor.Create(colorValue).Value;
            TailLength? tailLength = tailLengthValue is null ? null : TailLength.Create(tailLengthValue.Value).Value;
            DogWeight? weight = weightValue is null ? null : DogWeight.Create(weightValue.Value).Value;

            // Act
            Action act = () => Dog.Create(id, name!, color!, tailLength!, weight!);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
