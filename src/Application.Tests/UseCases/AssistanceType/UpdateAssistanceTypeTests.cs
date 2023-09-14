using Application.Interfaces.UseCases.AssistanceType;
using Application.Ports.AssistanceType;
using Application.UseCases.AssistanceType;
using Application.Validation;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.UseCases.AssistanceType
{
    public class UpdateAssistanceTypeTests
    {
        private readonly Mock<IAssistanceTypeRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IUpdateAssistanceType CreateUseCase()
        {
            return new UpdateAssistanceType(_repositoryMock.Object, _mapperMock.Object);
        }

        private Domain.Entities.AssistanceType MockValidAssistanceType() => new("AssistanceTypeName1", "AssistanceTypeDescription");
        private UpdateAssistanceTypeInput MockValidAssistanceTypeInput() => new UpdateAssistanceTypeInput
        {
            Name = "AssistanceTypeName2",
            Description = "AssistanceTypeDescription"
        };

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadAssistanceTypeOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var input = MockValidAssistanceTypeInput();
            var existingAssistanceType = MockValidAssistanceType();

            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(existingAssistanceType);
            _repositoryMock.Setup(repo => repo.GetAssistanceTypeByNameAsync(input.Name)).ReturnsAsync((Domain.Entities.AssistanceType)null);
            _repositoryMock.Setup(repo => repo.UpdateAsync(existingAssistanceType)).ReturnsAsync(existingAssistanceType);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadAssistanceTypeOutput>(existingAssistanceType)).Returns(new DetailedReadAssistanceTypeOutput());

            // Act
            var result = await useCase.ExecuteAsync(id, input);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _repositoryMock.Verify(repo => repo.GetAssistanceTypeByNameAsync(input.Name), Times.Once);
            _repositoryMock.Verify(repo => repo.UpdateAsync(existingAssistanceType), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadAssistanceTypeOutput>(existingAssistanceType), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_IdIsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = MockValidAssistanceTypeInput();

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(null, input));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _repositoryMock.Verify(repo => repo.GetAssistanceTypeByNameAsync(It.IsAny<string>()), Times.Never);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.AssistanceType>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadAssistanceTypeOutput>(It.IsAny<Domain.Entities.AssistanceType>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_NameIsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var input = new UpdateAssistanceTypeInput
            {
                Name = null
            };
            var existingAssistanceType = MockValidAssistanceType();

            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(existingAssistanceType);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Never);
            _repositoryMock.Verify(repo => repo.GetAssistanceTypeByNameAsync(It.IsAny<string>()), Times.Never);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.AssistanceType>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadAssistanceTypeOutput>(It.IsAny<Domain.Entities.AssistanceType>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_AssistanceTypeWithNameExists_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var input = MockValidAssistanceTypeInput();
            var existingAssistanceType = MockValidAssistanceType();

            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(existingAssistanceType);
            _repositoryMock.Setup(repo => repo.GetAssistanceTypeByNameAsync(input.Name)).ReturnsAsync(MockValidAssistanceType());

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _repositoryMock.Verify(repo => repo.GetAssistanceTypeByNameAsync(input.Name), Times.Once);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.AssistanceType>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadAssistanceTypeOutput>(It.IsAny<Domain.Entities.AssistanceType>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_AssistanceTypeIsDeleted_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var input = MockValidAssistanceTypeInput();
            var existingAssistanceType = MockValidAssistanceType();
            existingAssistanceType.DeactivateEntity();

            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(existingAssistanceType);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _repositoryMock.Verify(repo => repo.GetAssistanceTypeByNameAsync(It.IsAny<string>()), Times.Never);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.AssistanceType>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadAssistanceTypeOutput>(It.IsAny<Domain.Entities.AssistanceType>()), Times.Never);
        }
    }
}
