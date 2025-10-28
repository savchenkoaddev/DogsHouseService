using DogsHouseService.Application.UseCases.Dogs.Queries.GetDogs;
using DogsHouseService.Testing.Shared.Factories;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace DogsHouseService.Application.UnitTests.Validators
{
    public class GetDogsQueryValidatorTests
    {
        private readonly GetDogsQueryValidator _validator;

        public GetDogsQueryValidatorTests()
        {
            _validator = new GetDogsQueryValidator();
        }

        [Theory]
        [InlineData("asc")]
        [InlineData("desc")]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_IfOrderIsValid_ShouldNotReturnError(string order)
        {
            // Arrange
            var query = QueryFixtureFactory.BuildGetDogsQuery(order: order);

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Order);
        }

        [Fact]
        public void Validate_IfOrderIsInvalid_ShouldReturnError()
        {
            // Arrange
            var query = QueryFixtureFactory.BuildGetDogsQuery(order: "invalid");

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Order);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Validate_IfPageNumberIsInvalid_ShouldReturnError(int pageNumber)
        {
            // Arrange
            var query = QueryFixtureFactory.BuildGetDogsQuery(
                pageNumber: pageNumber);

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.PageNumber);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Validate_IfPageSizeIsInvalid_ShouldReturnError(int pageSize)
        {
            // Arrange
            var query = QueryFixtureFactory.BuildGetDogsQuery(
                pageSize: pageSize);

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.PageSize);
        }

        [Theory]
        [InlineData("id")]
        [InlineData("name")]
        [InlineData("color")]
        [InlineData("taillength")]
        [InlineData("tail-length")]
        [InlineData("tail_length")]
        [InlineData("weight")]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_IfAttributeIsValid_ShouldNotReturnError(string attribute)
        {
            // Arrange
            var query = QueryFixtureFactory.BuildGetDogsQuery(
                attribute: attribute);

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Attribute);
        }

        [Fact]
        public void Validate_IfAttributeIsInvalid_ShouldReturnError()
        {
            // Arrange
            var query = QueryFixtureFactory.BuildGetDogsQuery(
                attribute: "unknown");

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Attribute);
        }

        [Fact]
        public void Validate_IfAllFieldsAreValid_ShouldNotReturnError()
        {
            // Arrange
            var query = QueryFixtureFactory.BuildGetDogsQuery();

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.IsValid.Should().BeTrue();
        }
    }
}
