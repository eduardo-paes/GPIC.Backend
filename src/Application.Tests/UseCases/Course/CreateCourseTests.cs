using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Ports.Course;
using Application.Interfaces.UseCases.Course;
using Application.Validation;
using Moq;
using Xunit;
using Application.UseCases.Course;

namespace Application.Tests.UseCases.Course
{
    public class CreateCourseTests
    {
        private readonly Mock<ICourseRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private ICreateCourse CreateUseCase() => new CreateCourse(_repositoryMock.Object, _mapperMock.Object);

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadCourseOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new CreateCourseInput
            {
                Name = "New Course Name"
            };
            Domain.Entities.Course createdCourse = new("New Course Name");

            _repositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Course>())).ReturnsAsync(createdCourse);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadCourseOutput>(It.IsAny<Domain.Entities.Course>())).Returns(new DetailedReadCourseOutput());

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(repo => repo.GetCourseByNameAsync(input.Name), Times.Once);
            _repositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Course>()), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadCourseOutput>(It.IsAny<Domain.Entities.Course>()), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_NameIsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new CreateCourseInput
            {
                Name = null
            };

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _repositoryMock.Verify(repo => repo.GetCourseByNameAsync(It.IsAny<string>()), Times.Never);
            _repositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Course>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadCourseOutput>(It.IsAny<Domain.Entities.Course>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_CourseWithNameExists_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new CreateCourseInput
            {
                Name = "Existing Course Name"
            };
            var existingCourse = new Domain.Entities.Course("Existing Course Name");

            _repositoryMock.Setup(repo => repo.GetCourseByNameAsync(input.Name)).ReturnsAsync(existingCourse);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _repositoryMock.Verify(repo => repo.GetCourseByNameAsync(input.Name), Times.Once);
            _repositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Course>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadCourseOutput>(It.IsAny<Domain.Entities.Course>()), Times.Never);
        }
    }
}
