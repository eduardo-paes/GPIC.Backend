using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Course;
using Application.Ports.Course;
using Application.Validation;
using Moq;
using Xunit;
using Application.UseCases.Course;

namespace Application.Tests.UseCases.Course
{
    public class GetCourseByIdTests
    {
        private readonly Mock<ICourseRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IGetCourseById CreateUseCase() => new GetCourseById(_repositoryMock.Object, _mapperMock.Object);

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadCourseOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var expectedCourse = new Domain.Entities.Course("Course Name");
            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(expectedCourse);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadCourseOutput>(expectedCourse)).Returns(new DetailedReadCourseOutput());

            // Act
            var result = await useCase.ExecuteAsync(id);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadCourseOutput>(expectedCourse), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_IdIsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(null));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid?>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadCourseOutput>(It.IsAny<Domain.Entities.Course>()), Times.Never);
        }
    }
}
