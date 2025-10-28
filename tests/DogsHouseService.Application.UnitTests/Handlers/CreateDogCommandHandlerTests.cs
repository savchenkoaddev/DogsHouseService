using DogsHouseService.Application.Abstractions.Data;
using DogsHouseService.Application.UseCases.Dogs.Commands.CreateDog;
using DogsHouseService.Domain.Dogs;
using DogsHouseService.Domain.Dogs.DogNames;
using DogsHouseService.SharedKernel.Results;
using DogsHouseService.Testing.Shared.Factories;
using FluentAssertions;
using Moq;

namespace DogsHouseService.Application.UnitTests.Handlers
{
    public sealed class CreateDogCommandHandlerTests
    {
        private readonly Mock<IDogRepository> _dogRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<Mapper<CreateDogCommand, Result<Dog>>> _mapperMock;
        private readonly CreateDogCommandHandler _handler;

        public CreateDogCommandHandlerTests()
        {
            _dogRepositoryMock = new Mock<IDogRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<Mapper<CreateDogCommand, Result<Dog>>>();

            _handler = new CreateDogCommandHandler(
                _dogRepositoryMock.Object,
                _mapperMock.Object,
                _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_IfMappingFails_ShouldReturnFailure()
        {
            // Arrange
            var command = CommandFixtureFactory.BuildCreateDogCommand();
            var error = DogNameErrors.EmptyValue;

            _mapperMock.Setup(m => m.Map(command))
                .Returns(Result.Failure<Dog>(error));

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(error);
        }

        [Fact]
        public async Task Handle_IfDogWithSameNameExists_ShouldReturnDuplicateNameError()
        {
            // Arrange
            var command = CommandFixtureFactory.BuildCreateDogCommand();
            var dog = DogFixtureFactory.CreateDog();

            _mapperMock.Setup(m => m.Map(command)).Returns(Result.Success(dog));
            _dogRepositoryMock.Setup(r => r.ExistsWithNameAsync(dog.Name, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(DogErrors.DuplicateName(dog.Name));
        }

        [Fact]
        public async Task Handle_IfDogIsValid_ShouldInsertDogAndReturnSuccess()
        {
            // Arrange
            var command = CommandFixtureFactory.BuildCreateDogCommand();
            var dog = DogFixtureFactory.CreateDog();

            _mapperMock.Setup(m => m.Map(command)).Returns(Result.Success(dog));
            _dogRepositoryMock.Setup(r => r.ExistsWithNameAsync(dog.Name, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _dogRepositoryMock.Setup(r => r.InsertAsync(dog, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(dog.Id.Value);

            _dogRepositoryMock.Verify(r => r.InsertAsync(dog, It.IsAny<CancellationToken>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
