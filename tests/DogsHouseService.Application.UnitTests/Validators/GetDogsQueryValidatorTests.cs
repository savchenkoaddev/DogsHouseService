using DogsHouseService.Application.UseCases.Dogs.Queries.GetDogs;
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
            var query = new GetDogsQuery(
                Attribute: "name",
                Order: order,
                PageNumber: 1,
                PageSize: 10);

            var result = _validator.TestValidate(query);

            result.ShouldNotHaveValidationErrorFor(x => x.Order);
        }

        [Fact]
        public void Validate_IfOrderIsInvalid_ShouldReturnError()
        {
            var query = new GetDogsQuery(
                Attribute: "name",
                Order: "invalid",
                PageNumber: 1,
                PageSize: 10);

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(x => x.Order);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Validate_IfPageNumberIsInvalid_ShouldReturnError(int pageNumber)
        {
            var query = new GetDogsQuery(
                Attribute: "name",
                Order: "asc",
                PageNumber: pageNumber,
                PageSize: 10);

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(x => x.PageNumber);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Validate_IfPageSizeIsInvalid_ShouldReturnError(int pageSize)
        {
            var query = new GetDogsQuery(
                Attribute: "name",
                Order: "asc",
                PageNumber: 1,
                PageSize: pageSize);

            var result = _validator.TestValidate(query);

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
            var query = new GetDogsQuery(
                Attribute: attribute,
                Order: "asc",
                PageNumber: 1,
                PageSize: 10);

            var result = _validator.TestValidate(query);

            result.ShouldNotHaveValidationErrorFor(x => x.Attribute);
        }

        [Fact]
        public void Validate_IfAttributeIsInvalid_ShouldReturnError()
        {
            var query = new GetDogsQuery(
                Attribute: "unknown",
                Order: "asc",
                PageNumber: 1,
                PageSize: 10);

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(x => x.Attribute);
        }

        [Fact]
        public void Validate_IfAllFieldsAreValid_ShouldNotReturnError()
        {
            var query = new GetDogsQuery(
                Attribute: "name",
                Order: "asc",
                PageNumber: 1,
                PageSize: 10);

            var result = _validator.TestValidate(query);

            result.IsValid.Should().BeTrue();
        }
    }
}
