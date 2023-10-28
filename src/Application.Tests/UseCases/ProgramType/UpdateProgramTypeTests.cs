using AutoMapper;
using Application.UseCases.ProgramType;
using Application.Ports.ProgramType;
using Application.Validation;
using Domain.Interfaces.Repositories;
using Moq;
using Xunit;
using Application.Interfaces.UseCases.ProgramType;

namespace Application.Tests.UseCases.ProgramType
{
    public class UpdateProgramTypeTests
    {
        private readonly Mock<IProgramTypeRepository> _repositoryMock = new Mock<IProgramTypeRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        private IUpdateProgramType CreateUseCase() => new UpdateProgramType(_repositoryMock.Object, _mapperMock.Object);
        private static Domain.Entities.ProgramType MockValidProgramType() => new("Program Name", "Program Description");

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadProgramTypeOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid id = Guid.NewGuid();
            var input = new UpdateProgramTypeInput
            {
                Name = "Updated Program Type Name",
                Description = "Updated Program Type Description"
            };
            var existingProgramType = MockValidProgramType();

            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(existingProgramType);
            _repositoryMock.Setup(repo => repo.GetProgramTypeByNameAsync(input.Name)).ReturnsAsync((Domain.Entities.ProgramType)null);
            _repositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.ProgramType>())).ReturnsAsync(existingProgramType);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadProgramTypeOutput>(existingProgramType))
                .Returns(new DetailedReadProgramTypeOutput());

            // Act
            var result = await useCase.ExecuteAsync(id, input);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _repositoryMock.Verify(repo => repo.GetProgramTypeByNameAsync(input.Name), Times.Once);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.ProgramType>()), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProgramTypeOutput>(existingProgramType), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_InvalidId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid? id = null;
            var input = new UpdateProgramTypeInput
            {
                Name = "Updated Program Type Name",
                Description = "Updated Program Type Description"
            };

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(id, input));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _repositoryMock.Verify(repo => repo.GetProgramTypeByNameAsync(It.IsAny<string>()), Times.Never);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.ProgramType>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProgramTypeOutput>(It.IsAny<Domain.Entities.ProgramType>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_NameIsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid id = Guid.NewGuid();
            var input = new UpdateProgramTypeInput
            {
                Name = null,
                Description = "Updated Program Type Description"
            };
            var existingProgramType = MockValidProgramType();

            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(existingProgramType);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(id, input));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Never);
            _repositoryMock.Verify(repo => repo.GetProgramTypeByNameAsync(It.IsAny<string>()), Times.Never);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.ProgramType>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProgramTypeOutput>(It.IsAny<Domain.Entities.ProgramType>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_ProgramTypeNameExists_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid id = Guid.NewGuid();
            var input = new UpdateProgramTypeInput
            {
                Name = "Existing Program Type Name",
                Description = "Updated Program Type Description"
            };
            var existingProgramType = MockValidProgramType();

            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(existingProgramType);
            _repositoryMock.Setup(repo => repo.GetProgramTypeByNameAsync(input.Name)).ReturnsAsync(existingProgramType);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(id, input));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _repositoryMock.Verify(repo => repo.GetProgramTypeByNameAsync(input.Name), Times.Once);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.ProgramType>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProgramTypeOutput>(It.IsAny<Domain.Entities.ProgramType>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_DeletedProgramType_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid id = Guid.NewGuid();
            var input = new UpdateProgramTypeInput
            {
                Name = "Updated Program Type Name",
                Description = "Updated Program Type Description"
            };
            var existingProgramType = MockValidProgramType();
            existingProgramType.DeactivateEntity();

            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(existingProgramType);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(id, input));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _repositoryMock.Verify(repo => repo.GetProgramTypeByNameAsync(It.IsAny<string>()), Times.Never);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.ProgramType>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProgramTypeOutput>(It.IsAny<Domain.Entities.ProgramType>()), Times.Never);
        }
    }
}
