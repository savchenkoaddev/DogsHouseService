using DogsHouseService.Application.UseCases.Dogs.Commands.CreateDog;
using DogsHouseService.Application.UseCases.Dogs.Mappers;
using DogsHouseService.Domain.Dogs.DogColors;
using DogsHouseService.Domain.Dogs.DogNames;
using DogsHouseService.Domain.Dogs.DogWeights;
using DogsHouseService.Domain.Dogs.TailLengths;
using FluentAssertions;

namespace DogsHouseService.Application.UnitTests.Mappers
{
    public sealed class CreateDogCommandToEntityMapperTests
    {
        private readonly CreateDogCommandToEntityMapper _mapper;

        public CreateDogCommandToEntityMapperTests()
        {
            _mapper = new CreateDogCommandToEntityMapper();
        }

        [Fact]
        public void Map_InvalidName_ReturnsFailure()
        {
            // Arrange
            var command = new CreateDogCommand(
                Name: "",
                Color: "Red",
                TailLength: 50,
                Weight: 20);

            // Act
            var result = _mapper.Map(command);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(DogNameErrors.EmptyValue);
        }

        [Fact]
        public void Map_InvalidColor_ReturnsFailure()
        {
            var command = new CreateDogCommand(
                Name: "Doggy",
                Color: "",
                TailLength: 50,
                Weight: 20);

            var result = _mapper.Map(command);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(DogColorErrors.EmptyValue);
        }

        [Fact]
        public void Map_InvalidTailLength_ReturnsFailure()
        {
            var command = new CreateDogCommand(
                Name: "Doggy",
                Color: "Red",
                TailLength: -1,
                Weight: 20);

            var result = _mapper.Map(command);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(TailLengthErrors.TooSmall(TailLength.MinLength));
        }

        [Fact]
        public void Map_InvalidWeight_ReturnsFailure()
        {
            var command = new CreateDogCommand(
                Name: "Doggy",
                Color: "Red",
                TailLength: 1,
                Weight: -20);

            var result = _mapper.Map(command);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(DogWeightErrors.TooSmall(DogWeight.MinValue));
        }

        [Fact]
        public void Map_ValidCommand_ReturnsSuccessDog()
        {
            // Arrange
            var command = new CreateDogCommand(
                Name: "Doggy",
                Color: "Red",
                TailLength: 1,
                Weight: 20);

            // Act
            var result = _mapper.Map(command);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Name.Value.Should().Be(command.Name);
            result.Value.Color.Value.Should().Be(command.Color);
            result.Value.TailLength.Value.Should().Be(command.TailLength);
            result.Value.Weight.Value.Should().Be(command.Weight);
        }
    }
}
