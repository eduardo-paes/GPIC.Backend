using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.UseCases.Professor;
using Application.Ports.Professor;
using Application.Validation;
using Moq;
using Xunit;
using Application.Interfaces.UseCases.Professor;

namespace Application.Tests.UseCases.Professor
{
    public class GetProfessorByIdTests
    {
        private readonly Mock<IProfessorRepository> _repositoryMock = new Mock<IProfessorRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        private IGetProfessorById CreateUseCase() => new GetProfessorById(_repositoryMock.Object, _mapperMock.Object);

        [Fact]
        public async Task ExecuteAsync_ValidId_ReturnsDetailedReadProfessorOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid professorId = Guid.NewGuid();
            var professorEntity = new Domain.Entities.Professor("1234567", 12345); // Create a Professor entity for testing
            var expectedOutput = new DetailedReadProfessorOutput(); // Create an expected output

            _repositoryMock.Setup(repo => repo.GetByIdAsync(professorId)).ReturnsAsync(professorEntity);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadProfessorOutput>(professorEntity)).Returns(expectedOutput);

            // Act
            var result = await useCase.ExecuteAsync(professorId);

            // Assert
            Assert.NotNull(result);
            Assert.Same(expectedOutput, result); // Check if the expected output is returned
        }

        [Fact]
        public async Task ExecuteAsync_NullId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid? professorId = null;

            // Act & Assert
            await Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(professorId));
        }
    }
}
