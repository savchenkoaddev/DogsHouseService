using DogsHouseService.Application.UseCases.Dogs.Commands.CreateDog;
using DogsHouseService.Domain.Dogs.DogNames;
using DogsHouseService.Domain.Dogs.DogWeights;
using DogsHouseService.Domain.Dogs.TailLengths;
using DogsHouseService.Testing.Shared.Factories;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace DogsHouseService.Application.UnitTests.Validators
{
    public sealed class CreateDogCommandValidatorTests
    {
        private readonly CreateDogCommandValidator _validator;

        public CreateDogCommandValidatorTests()
        {
            _validator = new CreateDogCommandValidator();
        }

        [Fact]
        public void Validate_IfNameIsEmpty_ShouldReturnError()
        {
            // Arrange
            var command = CommandFixtureFactory.BuildCreateDogCommand(name: "");

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_IfNameLengthExceededMaxLength_ShouldReturnTooLongError()
        {
            // Arrange
            var longName = new string('a', DogName.MaxLength + 1);
            var command = CommandFixtureFactory.BuildCreateDogCommand(name: longName);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_IfColorIsEmpty_ShouldReturnError()
        {
            // Arrange
            var command = CommandFixtureFactory.BuildCreateDogCommand(color: "");

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Color);
        }

        [Fact]
        public void Validate_IfTailLengthBelowMinimum_ShouldReturnTooSmallError()
        {
            // Arrange
            var command = CommandFixtureFactory.BuildCreateDogCommand(
                tailLength: TailLength.MinLength - 1);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.TailLength);
        }

        [Fact]
        public void Validate_IfTailLengthAboveMaximum_ShouldReturnTooBigError()
        {
            // Arrange
            var command = CommandFixtureFactory.BuildCreateDogCommand(
                tailLength: TailLength.MaxLength + 1);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.TailLength);
        }

        [Fact]
        public void Validate_IfWeightBelowMinimum_ShouldReturnTooSmallError()
        {
            // Arrange
            var command = CommandFixtureFactory.BuildCreateDogCommand(
                weight: DogWeight.MinValue - 1);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Weight);
        }

        [Fact]
        public void Validate_IfWeightAboveMaximum_ShouldReturnTooBigError()
        {
            // Arrange
            var command = CommandFixtureFactory.BuildCreateDogCommand(
                weight: DogWeight.MaxValue + 1);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Weight);
        }

        [Fact]
        public void Validate_IfAllFieldsAreValid_ShouldNotReturnError()
        {
            // Arrange
            var command = CommandFixtureFactory.BuildCreateDogCommand();

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.IsValid.Should().BeTrue();
        }
    }
}
