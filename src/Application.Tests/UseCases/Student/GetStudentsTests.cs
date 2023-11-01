using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Student;
using Application.Ports.Student;
using Moq;
using Xunit;
using Application.Tests.Mocks;

namespace Application.Tests.UseCases.Student
{
    public class GetStudentsTests
    {
        private readonly Mock<IStudentRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IGetStudents CreateUseCase() => new Application.UseCases.Student.GetStudents(
            _repositoryMock.Object,
            _mapperMock.Object
        );

        [Fact]
        public async Task ExecuteAsync_ValidParameters_ReturnsQueryable()
        {
            // Arrange
            var useCase = CreateUseCase();
            int skip = 0;
            int take = 10;
            var students = new List<Domain.Entities.Student> { StudentMock.MockValidStudent() };
            var mappedStudents = new List<ResumedReadStudentOutput> { new() };

            _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(students);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ResumedReadStudentOutput>>(It.IsAny<IEnumerable<Domain.Entities.Student>>()))
                .Returns(mappedStudents.AsEnumerable());

            // Act
            var result = await useCase.ExecuteAsync(skip, take);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(repo => repo.GetAllAsync(skip, take), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ResumedReadStudentOutput>>(students), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_InvalidSkipValue_ThrowsArgumentException()
        {
            // Arrange
            var useCase = CreateUseCase();
            int skip = -1;
            int take = 10;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await useCase.ExecuteAsync(skip, take));

            // No need to verify any repository or mapper calls in this case.
        }

        [Fact]
        public async Task ExecuteAsync_InvalidTakeValue_ThrowsArgumentException()
        {
            // Arrange
            var useCase = CreateUseCase();
            int skip = 0;
            int take = 0;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await useCase.ExecuteAsync(skip, take));

            // No need to verify any repository or mapper calls in this case.
        }
    }
}
