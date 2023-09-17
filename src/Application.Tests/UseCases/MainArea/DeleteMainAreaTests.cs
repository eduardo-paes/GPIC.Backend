using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.MainArea;
using Application.Ports.MainArea;
using Application.UseCases.MainArea;
using Application.Validation;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.MainArea
{
    public class DeleteMainAreaTests
    {
        private readonly Mock<IMainAreaRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IDeleteMainArea CreateUseCase() => new DeleteMainArea(_repositoryMock.Object, _mapperMock.Object);

        [Fact]
        public async Task ExecuteAsync_ValidId_ReturnsDetailedMainAreaOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid(); // Set the ID to a valid one
            Domain.Entities.MainArea deletedMainArea = new(id, "Code", "Name"); // Create a deleted main area object

            _repositoryMock.Setup(repo => repo.DeleteAsync(id)).ReturnsAsync(deletedMainArea);
            _mapperMock.Setup(mapper => mapper.Map<DetailedMainAreaOutput>(deletedMainArea)).Returns(new DetailedMainAreaOutput()); // You can create a new instance here.

            // Act
            var result = await useCase.ExecuteAsync(id);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(repo => repo.DeleteAsync(id), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedMainAreaOutput>(deletedMainArea), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_NullId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid? id = null; // Set the ID to null

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id));
            _repositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Guid>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedMainAreaOutput>(It.IsAny<Domain.Entities.MainArea>()), Times.Never);
        }
    }
}
