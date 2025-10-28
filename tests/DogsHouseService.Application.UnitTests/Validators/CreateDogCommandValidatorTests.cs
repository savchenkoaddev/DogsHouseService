using DogsHouseService.Application.UseCases.Dogs.Commands.CreateDog;
using DogsHouseService.Domain.Dogs.DogNames;
using DogsHouseService.Domain.Dogs.DogWeights;
using DogsHouseService.Domain.Dogs.TailLengths;
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
            var command = new CreateDogCommand(
                Name: "",
                Color: "Red",
                TailLength: 10,
                Weight: 20);

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_IfNameLengthExceededMaxLength_ShouldReturnTooLongError()
        {
            var longName = new string('a', DogName.MaxLength + 1);
            var command = new CreateDogCommand(
                Name: longName,
                Color: "Red",
                TailLength: 10,
                Weight: 20);

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validate_IfColorIsEmpty_ShouldReturnError()
        {
            var command = new CreateDogCommand(
                Name: "Neo",
                Color: "",
                TailLength: 10,
                Weight: 20);

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Color);
        }

        [Fact]
        public void Validate_IfTailLengthBelowMinimum_ShouldReturnTooSmallError()
        {
            var command = new CreateDogCommand(
                Name: "Neo",
                Color: "Red",
                TailLength: TailLength.MinLength - 1,
                Weight: 20);

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.TailLength);
        }

        [Fact]
        public void Validate_IfTailLengthAboveMaximum_ShouldReturnTooBigError()
        {
            var command = new CreateDogCommand(
                Name: "Neo",
                Color: "Red",
                TailLength: TailLength.MaxLength + 1,
                Weight: 20);

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.TailLength);
        }

        [Fact]
        public void Validate_IfWeightBelowMinimum_ShouldReturnTooSmallError()
        {
            var command = new CreateDogCommand(
                Name: "Neo",
                Color: "Red",
                TailLength: 10,
                Weight: DogWeight.MinValue - 1);

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Weight);
        }

        [Fact]
        public void Validate_IfWeightAboveMaximum_ShouldReturnTooBigError()
        {
            var command = new CreateDogCommand(
                Name: "Neo",
                Color: "Red",
                TailLength: 10,
                Weight: DogWeight.MaxValue + 1);

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Weight);
        }

        [Fact]
        public void Validate_IfAllFieldsAreValid_ShouldNotReturnError()
        {
            var command = new CreateDogCommand(
                Name: "Neo",
                Color: "Red",
                TailLength: 10,
                Weight: 20);

            var result = _validator.TestValidate(command);

            result.IsValid.Should().BeTrue();
        }
    }
}
