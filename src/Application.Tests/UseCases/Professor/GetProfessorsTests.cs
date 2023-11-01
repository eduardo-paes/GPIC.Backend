using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.UseCases.Professor;
using Application.Ports.Professor;
using Moq;
using Xunit;
using Application.Interfaces.UseCases.Professor;

namespace Application.Tests.UseCases.Professor
{
    public class GetProfessorsTests
    {
        private readonly Mock<IProfessorRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private static Domain.Entities.Professor MockValidProfessor() => new("1234567", 12345);

        private IGetProfessors CreateUseCase() => new GetProfessors(_repositoryMock.Object, _mapperMock.Object);

        [Fact]
        public async Task ExecuteAsync_ValidParameters_ReturnsQueryableOfResumedReadProfessorOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            int skip = 0;
            int take = 10;
            var professorEntities = new List<Domain.Entities.Professor> { MockValidProfessor(), MockValidProfessor() }.AsQueryable();
            var expectedOutput = professorEntities.Select(p => new ResumedReadProfessorOutput()).AsQueryable();

            _repositoryMock.Setup(repo => repo.GetAllAsync(skip, take)).ReturnsAsync(professorEntities);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ResumedReadProfessorOutput>>(professorEntities)).Returns(expectedOutput);

            // Act
            var result = await useCase.ExecuteAsync(skip, take);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IQueryable<ResumedReadProfessorOutput>>(result);
            // Check if the collections have the same number of elements
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
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ResumedReadProfessorOutput>>(It.IsAny<IEnumerable<Domain.Entities.Professor>>()), Times.Never);
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
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ResumedReadProfessorOutput>>(It.IsAny<IEnumerable<Domain.Entities.Professor>>()), Times.Never);
        }
    }
}
