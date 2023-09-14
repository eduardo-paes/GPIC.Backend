using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Campus;
using Application.Ports.Campus;
using Application.Validation;
using Moq;
using Xunit;
using Application.UseCases.Campus;

namespace Application.Tests.UseCases.Campus
{
    public class UpdateCampusTests
    {
        private readonly Mock<ICampusRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IUpdateCampus CreateUseCase() => new UpdateCampus(_repositoryMock.Object, _mapperMock.Object);

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadCampusOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var input = new UpdateCampusInput
            {
                Name = "Updated Campus Name"
            };
            var existingCampus = new Domain.Entities.Campus("Initial Campus Name");

            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(existingCampus);
            _repositoryMock.Setup(repo => repo.GetCampusByNameAsync(input.Name)).ReturnsAsync((Domain.Entities.Campus)null);
            _repositoryMock.Setup(repo => repo.UpdateAsync(existingCampus)).ReturnsAsync(existingCampus);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadCampusOutput>(existingCampus)).Returns(new DetailedReadCampusOutput());

            // Act
            var result = await useCase.ExecuteAsync(id, input);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _repositoryMock.Verify(repo => repo.GetCampusByNameAsync(input.Name), Times.Once);
            _repositoryMock.Verify(repo => repo.UpdateAsync(existingCampus), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadCampusOutput>(existingCampus), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_IdIsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new UpdateCampusInput
            {
                Name = "Updated Campus Name"
            };

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(null, input));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _repositoryMock.Verify(repo => repo.GetCampusByNameAsync(It.IsAny<string>()), Times.Never);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Campus>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadCampusOutput>(It.IsAny<Domain.Entities.Campus>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_NameIsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var input = new UpdateCampusInput
            {
                Name = null
            };
            var existingCampus = new Domain.Entities.Campus("Initial Campus Name");

            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(existingCampus);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Never);
            _repositoryMock.Verify(repo => repo.GetCampusByNameAsync(It.IsAny<string>()), Times.Never);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Campus>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadCampusOutput>(It.IsAny<Domain.Entities.Campus>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_CampusWithNameExists_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var input = new UpdateCampusInput
            {
                Name = "Existing Campus Name"
            };
            var existingCampus = new Domain.Entities.Campus("Existing Campus Name");
            var anotherCampus = new Domain.Entities.Campus("Another Campus Name");

            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(anotherCampus);
            _repositoryMock.Setup(repo => repo.GetCampusByNameAsync(input.Name)).ReturnsAsync(existingCampus);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _repositoryMock.Verify(repo => repo.GetCampusByNameAsync(input.Name), Times.Once);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Campus>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadCampusOutput>(It.IsAny<Domain.Entities.Campus>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_CampusIsDeleted_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var input = new UpdateCampusInput
            {
                Name = "Updated Campus Name"
            };
            var existingCampus = new Domain.Entities.Campus("Initial Campus Name");
            existingCampus.DeactivateEntity(); // Marcando como excluído

            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(existingCampus);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _repositoryMock.Verify(repo => repo.GetCampusByNameAsync(input.Name), Times.Never);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Campus>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadCampusOutput>(It.IsAny<Domain.Entities.Campus>()), Times.Never);
        }
    }
}
