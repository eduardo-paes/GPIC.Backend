using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Validation;
using Moq;
using Xunit;
using Application.UseCases.Professor;
using Application.Interfaces.UseCases.Professor;
using Application.Ports.Professor;

namespace Application.Tests.UseCases.Professor
{
    public class UpdateProfessorTests
    {
        private readonly Mock<IProfessorRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IUpdateProfessor CreateUseCase() => new UpdateProfessor(_repositoryMock.Object, _mapperMock.Object);

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadProfessorOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid professorId = Guid.NewGuid();
            var updateModel = new UpdateProfessorInput
            {
                IdentifyLattes = 1234567,
                SIAPEEnrollment = "1234567"
            };
            var professorEntity = new Domain.Entities.Professor
            (
                id: professorId,
                identifyLattes: 7654321,
                siapeEnrollment: "1234567"
            );
            var expectedOutput = new DetailedReadProfessorOutput();

            _repositoryMock.Setup(repo => repo.GetByIdAsync(professorId)).ReturnsAsync(professorEntity);
            _repositoryMock.Setup(repo => repo.UpdateAsync(professorEntity)).ReturnsAsync(professorEntity);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadProfessorOutput>(professorEntity)).Returns(expectedOutput);

            // Act
            var result = await useCase.ExecuteAsync(professorId, updateModel);

            // Assert
            Assert.NotNull(result);
            Assert.Same(expectedOutput, result);
        }

        [Fact]
        public void ExecuteAsync_NullId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid? professorId = null;
            var updateModel = new UpdateProfessorInput();

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(professorId, updateModel));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Professor>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProfessorOutput>(It.IsAny<Domain.Entities.Professor>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_ProfessorNotFound_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid professorId = Guid.NewGuid();
            var updateModel = new UpdateProfessorInput();

            _repositoryMock.Setup(repo => repo.GetByIdAsync(professorId)).ReturnsAsync((Domain.Entities.Professor)null);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(professorId, updateModel));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(professorId), Times.Once);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Professor>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProfessorOutput>(It.IsAny<Domain.Entities.Professor>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_DeletedProfessor_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid professorId = Guid.NewGuid();
            var updateModel = new UpdateProfessorInput();
            var professorEntity = new Domain.Entities.Professor
            (
                id: professorId,
                identifyLattes: 7654321,
                siapeEnrollment: "1234567"
            );
            professorEntity.DeactivateEntity();

            _repositoryMock.Setup(repo => repo.GetByIdAsync(professorId)).ReturnsAsync(professorEntity);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(professorId, updateModel));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(professorId), Times.Once);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Professor>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProfessorOutput>(It.IsAny<Domain.Entities.Professor>()), Times.Never);
        }
    }
}
