using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.MainArea;
using Application.Ports.MainArea;
using Application.UseCases.MainArea;
using Moq;
using Xunit;
using Application.Validation;

namespace Application.Tests.UseCases.MainArea
{
    public class UpdateMainAreaTests
    {
        private readonly Mock<IMainAreaRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IUpdateMainArea CreateUseCase() => new UpdateMainArea(_repositoryMock.Object, _mapperMock.Object);

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedMainAreaOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid(); // Set the ID to an existing main area
            var input = new UpdateMainAreaInput
            {
                Name = "Updated Name",
                Code = "Updated Code"
            };
            var existingMainArea = new Domain.Entities.MainArea(id, "Existing Name", "Existing Code");

            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(existingMainArea);
            _repositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.MainArea>())).ReturnsAsync(existingMainArea); // You can modify this to return the updated main area
            _mapperMock.Setup(mapper => mapper.Map<DetailedMainAreaOutput>(It.IsAny<Domain.Entities.MainArea>())).Returns(new DetailedMainAreaOutput());

            // Act
            var result = await useCase.ExecuteAsync(id, input);

            // Assert
            Assert.NotNull(result);
            // Add assertions for the updated main area if necessary
            _repositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.MainArea>()), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedMainAreaOutput>(It.IsAny<Domain.Entities.MainArea>()), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_IdIsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = (Guid?)null;
            var input = new UpdateMainAreaInput
            {
                Name = "Updated Name",
                Code = "Updated Code"
            };

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.MainArea>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedMainAreaOutput>(It.IsAny<Domain.Entities.MainArea>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_MainAreaNotFound_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid(); // Set the ID to a non-existing main area
            var input = new UpdateMainAreaInput
            {
                Name = "Updated Name",
                Code = "Updated Code"
            };

            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Domain.Entities.MainArea)null);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.MainArea>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedMainAreaOutput>(It.IsAny<Domain.Entities.MainArea>()), Times.Never);
        }
    }
}
