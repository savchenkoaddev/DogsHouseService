using DogsHouseService.Application.Abstractions.Data;
using DogsHouseService.Application.UseCases.Dogs.Commands.CreateDog;
using DogsHouseService.Domain.Dogs;
using DogsHouseService.Domain.Dogs.DogColors;
using DogsHouseService.Domain.Dogs.DogNames;
using DogsHouseService.Domain.Dogs.DogWeights;
using DogsHouseService.Domain.Dogs.TailLengths;
using DogsHouseService.SharedKernel.Results;
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
            var command = new CreateDogCommand("Neo", "Red", 10, 20);
            var error = DogNameErrors.EmptyValue;

            _mapperMock.Setup(m => m.Map(command))
                .Returns(Result.Failure<Dog>(error));

            var result = await _handler.Handle(command, default);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(error);
        }

        [Fact]
        public async Task Handle_IfDogWithSameNameExists_ShouldReturnDuplicateNameError()
        {
            var command = new CreateDogCommand("Neo", "Red", 10, 20);
            var dog = Dog.Create(new DogId(Guid.NewGuid()),
                DogName.Create("Neo").Value,
                DogColor.Create("Red").Value,
                TailLength.Create(10).Value,
                DogWeight.Create(20).Value).Value;

            _mapperMock.Setup(m => m.Map(command)).Returns(Result.Success(dog));
            _dogRepositoryMock.Setup(r => r.ExistsWithNameAsync(dog.Name, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var result = await _handler.Handle(command, default);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(DogErrors.DuplicateName(dog.Name));
        }

        [Fact]
        public async Task Handle_IfDogIsValid_ShouldInsertDogAndReturnSuccess()
        {
            var command = new CreateDogCommand("Neo", "Red", 10, 20);
            var dog = Dog.Create(new DogId(Guid.NewGuid()),
                DogName.Create("Neo").Value,
                DogColor.Create("Red").Value,
                TailLength.Create(10).Value,
                DogWeight.Create(20).Value).Value;

            _mapperMock.Setup(m => m.Map(command)).Returns(Result.Success(dog));
            _dogRepositoryMock.Setup(r => r.ExistsWithNameAsync(dog.Name, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _dogRepositoryMock.Setup(r => r.InsertAsync(dog, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            var result = await _handler.Handle(command, default);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(dog.Id.Value);

            _dogRepositoryMock.Verify(r => r.InsertAsync(dog, It.IsAny<CancellationToken>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
