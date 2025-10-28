using DogsHouseService.Application.Abstractions.Data;
using DogsHouseService.Application.UseCases.Dogs.DTO;
using DogsHouseService.Application.UseCases.Dogs.Queries.GetDogs;
using DogsHouseService.Domain.Dogs;
using DogsHouseService.Testing.Shared.Factories;
using FluentAssertions;
using Moq;

namespace DogsHouseService.Application.UnitTests.Handlers
{
    public sealed class GetDogsQueryHandlerTests
    {
        private readonly Mock<IDogRepository> _dogRepositoryMock;
        private readonly Mock<Mapper<Dog, DogResponse>> _mapperMock;
        private readonly GetDogsQueryHandler _handler;

        public GetDogsQueryHandlerTests()
        {
            _dogRepositoryMock = new Mock<IDogRepository>();
            _mapperMock = new Mock<Mapper<Dog, DogResponse>>();

            _handler = new GetDogsQueryHandler(
                _dogRepositoryMock.Object,
                _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_IfValidRequest_ShouldReturnMappedDogs()
        {
            // Arrange
            var query = QueryFixtureFactory.BuildGetDogsQuery();

            var dogsFromRepo = new List<Dog>
            {
                DogFixtureFactory.CreateDog(),
                DogFixtureFactory.CreateDog()
            };

            var mappedDogs = new List<DogResponse>
            {
                new("Dog1", "Red", 10, 20),
                new("Dog2", "Blue", 15, 25)
            };

            _dogRepositoryMock.Setup(r => r.GetAllAsync(
                    query.Attribute,
                    true,
                    query.PageNumber,
                    query.PageSize,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(dogsFromRepo);

            _mapperMock.Setup(m => m.Map(dogsFromRepo))
                .Returns(mappedDogs);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(mappedDogs);

            _dogRepositoryMock.Verify(r => r.GetAllAsync(
                query.Attribute, true, query.PageNumber, query.PageSize, It.IsAny<CancellationToken>()), Times.Once);

            _mapperMock.Verify(m => m.Map(dogsFromRepo), Times.Once);
        }

        [Fact]
        public async Task Handle_IfNullOrder_ShouldSortAscendingByDefault()
        {
            // Arrange
            var query = QueryFixtureFactory.BuildGetDogsQuery(order: null);

            var dogsFromRepo = new List<Dog> {
                DogFixtureFactory.CreateDog()
            };

            var mappedDogs = new List<DogResponse> { new("Dog1", "Red", 10, 20) };

            _dogRepositoryMock.Setup(r => r.GetAllAsync(
                    query.Attribute,
                    false,
                    query.PageNumber,
                    query.PageSize,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(dogsFromRepo);

            _mapperMock.Setup(m => m.Map(dogsFromRepo))
                .Returns(mappedDogs);

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(mappedDogs);
        }
    }
}
