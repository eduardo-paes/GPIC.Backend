using AutoMapper;
using Domain.Interfaces.Repositories;
using Moq;
using Xunit;
using Application.UseCases.Student;
using Application.Ports.Student;
using Application.Validation;
using Application.Tests.Mocks;
using Application.Interfaces.UseCases.Student;

namespace Application.Tests.UseCases.Student
{
    public class GetStudentByIdTests
    {
        private readonly Mock<IStudentRepository> _studentRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IGetStudentById CreateUseCase() => new GetStudentById(
            _studentRepositoryMock.Object,
            _mapperMock.Object
        );

        [Fact]
        public async Task ExecuteAsync_ValidId_ReturnsDetailedReadStudentOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var studentId = Guid.NewGuid();
            var student = StudentMock.MockValidStudent();

            _studentRepositoryMock.Setup(repo => repo.GetByIdAsync(studentId)).ReturnsAsync(student);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadStudentOutput>(student)).Returns(new DetailedReadStudentOutput());

            // Act
            var result = await useCase.ExecuteAsync(studentId);

            // Assert
            Assert.NotNull(result);
            _studentRepositoryMock.Verify(repo => repo.GetByIdAsync(studentId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadStudentOutput>(student), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_NullId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();

            // Act & Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(null));
        }

        [Fact]
        public async Task ExecuteAsync_StudentNotFound_ReturnsNull()
        {
            // Arrange
            var useCase = CreateUseCase();
            var studentId = Guid.NewGuid();

            _studentRepositoryMock.Setup(repo => repo.GetByIdAsync(studentId)).ReturnsAsync((Domain.Entities.Student)null);

            // Act
            var result = await useCase.ExecuteAsync(studentId);

            // Assert
            Assert.Null(result);
            _studentRepositoryMock.Verify(repo => repo.GetByIdAsync(studentId), Times.Once);
        }
    }
}
