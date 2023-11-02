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
    public class CreateProgramTypeTests
    {
        private readonly Mock<IProgramTypeRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private ICreateProgramType CreateUseCase() => new CreateProgramType(_repositoryMock.Object, _mapperMock.Object);

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadProgramTypeOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new CreateProgramTypeInput
            {
                Name = "New Program Type Name"
            };
            var createdProgramType = new Domain.Entities.ProgramType
            (
                id: Guid.NewGuid(),
                name: "New Program Type Name",
                description: "New Program Type Description"
            );
            var expectedOutput = new DetailedReadProgramTypeOutput();

            _repositoryMock.Setup(repo => repo.GetProgramTypeByNameAsync(input.Name)).ReturnsAsync((Domain.Entities.ProgramType)null);
            _repositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.ProgramType>())).ReturnsAsync(createdProgramType);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadProgramTypeOutput>(createdProgramType)).Returns(expectedOutput);

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.NotNull(result);
            Assert.Same(expectedOutput, result);
        }

        [Fact]
        public void ExecuteAsync_NullName_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new CreateProgramTypeInput
            {
                Name = null
            };

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(input));
            _repositoryMock.Verify(repo => repo.GetProgramTypeByNameAsync(It.IsAny<string>()), Times.Never);
            _repositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.ProgramType>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProgramTypeOutput>(It.IsAny<Domain.Entities.ProgramType>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_ProgramTypeWithNameExists_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new CreateProgramTypeInput
            {
                Name = "Existing Program Type Name"
            };
            var existingProgramType = new Domain.Entities.ProgramType
            (
                id: Guid.NewGuid(),
                name: "Existing Program Type Name",
                description: "Existing Program Type Description"
            );

            _repositoryMock.Setup(repo => repo.GetProgramTypeByNameAsync(input.Name)).ReturnsAsync(existingProgramType);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(input));
            _repositoryMock.Verify(repo => repo.GetProgramTypeByNameAsync(input.Name), Times.Once);
            _repositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.ProgramType>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProgramTypeOutput>(It.IsAny<Domain.Entities.ProgramType>()), Times.Never);
        }
    }
}
