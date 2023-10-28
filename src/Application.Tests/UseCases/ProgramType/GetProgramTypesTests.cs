using AutoMapper;
using Application.UseCases.ProgramType;
using Application.Ports.ProgramType;
using Domain.Interfaces.Repositories;
using Moq;
using Xunit;
using Application.Interfaces.UseCases.ProgramType;

namespace Application.Tests.UseCases.ProgramType
{
    public class GetProgramTypesTests
    {
        private readonly Mock<IProgramTypeRepository> _repositoryMock = new Mock<IProgramTypeRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        private IGetProgramTypes CreateUseCase() => new GetProgramTypes(_repositoryMock.Object, _mapperMock.Object);
        private static Domain.Entities.ProgramType MockValidProgramType() => new("Program Name", "Program Description");

        [Fact]
        public async Task ExecuteAsync_ValidParameters_ReturnsQueryableOfResumedReadProgramTypeOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            int skip = 0;
            int take = 10;
            var programTypeEntities = new List<Domain.Entities.ProgramType> { MockValidProgramType(), MockValidProgramType() }.AsQueryable();
            var expectedOutput = programTypeEntities.Select(p => new ResumedReadProgramTypeOutput()).AsQueryable();

            _repositoryMock.Setup(repo => repo.GetAllAsync(skip, take)).ReturnsAsync(programTypeEntities);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ResumedReadProgramTypeOutput>>(programTypeEntities)).Returns(expectedOutput);

            // Act
            var result = await useCase.ExecuteAsync(skip, take);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IQueryable<ResumedReadProgramTypeOutput>>(result);
            Assert.Equal(expectedOutput.Count(), result.Count());
        }

        [Fact]
        public void ExecuteAsync_InvalidSkip_ThrowsArgumentException()
        {
            // Arrange
            var useCase = CreateUseCase();
            int skip = -1;
            int take = 10;

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(skip, take));
            _repositoryMock.Verify(repo => repo.GetAllAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ResumedReadProgramTypeOutput>>(It.IsAny<IEnumerable<Domain.Entities.ProgramType>>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_InvalidTake_ThrowsArgumentException()
        {
            // Arrange
            var useCase = CreateUseCase();
            int skip = 0;
            int take = 0;

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(skip, take));
            _repositoryMock.Verify(repo => repo.GetAllAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ResumedReadProgramTypeOutput>>(It.IsAny<IEnumerable<Domain.Entities.ProgramType>>()), Times.Never);
        }
    }
}
