using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Course;
using Application.Ports.Course;
using Application.UseCases.Course;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.Course
{
    public class GetCoursesTests
    {
        private readonly Mock<ICourseRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IGetCourses CreateUseCase() => new GetCourses(_repositoryMock.Object, _mapperMock.Object);

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsQueryableResumedReadCourseOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var skip = 0;
            var take = 10;
            var expectedCourses = new List<Domain.Entities.Course>
            {
                new("Course 1"),
                new("Course 2"),
                new("Course 3")
            };
            _repositoryMock.Setup(repo => repo.GetAllAsync(skip, take)).ReturnsAsync(expectedCourses);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ResumedReadCourseOutput>>(expectedCourses)).Returns(
                new List<ResumedReadCourseOutput>
                {
                    new(),
                    new(),
                    new()
                });

            // Act
            var result = await useCase.ExecuteAsync(skip, take);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            _repositoryMock.Verify(repo => repo.GetAllAsync(skip, take), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ResumedReadCourseOutput>>(expectedCourses), Times.Once);
        }
    }
}
