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
    public class DeleteProgramTypeTests
    {
        private readonly Mock<IProgramTypeRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IDeleteProgramType CreateUseCase() => new DeleteProgramType(_repositoryMock.Object, _mapperMock.Object);

        [Fact]
        public async Task ExecuteAsync_ValidId_ReturnsDetailedReadProgramTypeOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid id = Guid.NewGuid();
            var programType = new Domain.Entities.ProgramType
            (
                id: Guid.NewGuid(),
                name: "Program Type Name",
                description: "Program Type Description"
            );
            var expectedOutput = new DetailedReadProgramTypeOutput();

            _repositoryMock.Setup(repo => repo.DeleteAsync(id)).ReturnsAsync(programType);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadProgramTypeOutput>(programType)).Returns(expectedOutput);

            // Act
            var result = await useCase.ExecuteAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Same(expectedOutput, result);
        }

        [Fact]
        public void ExecuteAsync_NullId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid? id = null;

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(id));
            _repositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Guid?>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProgramTypeOutput>(It.IsAny<Domain.Entities.ProgramType>()), Times.Never);
        }
    }
}
