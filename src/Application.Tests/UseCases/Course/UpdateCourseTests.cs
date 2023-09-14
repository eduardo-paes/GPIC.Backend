using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Course;
using Application.Ports.Course;
using Application.UseCases.Course;
using Moq;
using Xunit;
using Application.Validation;

namespace Application.Tests.UseCases.Course
{
    public class UpdateCourseTests
    {
        private readonly Mock<ICourseRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IUpdateCourse CreateUseCase() => new UpdateCourse(_repositoryMock.Object, _mapperMock.Object);

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadCourseOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var input = new UpdateCourseInput
            {
                Name = "Updated Course Name"
            };
            var existingCourse = new Domain.Entities.Course("Existing Course Name");

            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(existingCourse);
            _repositoryMock.Setup(repo => repo.GetCourseByNameAsync(input.Name)).ReturnsAsync((Domain.Entities.Course)null);
            _repositoryMock.Setup(repo => repo.UpdateAsync(existingCourse)).ReturnsAsync(existingCourse);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadCourseOutput>(existingCourse)).Returns(new DetailedReadCourseOutput());

            // Act
            var result = await useCase.ExecuteAsync(id, input);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _repositoryMock.Verify(repo => repo.GetCourseByNameAsync(input.Name), Times.Once);
            _repositoryMock.Verify(repo => repo.UpdateAsync(existingCourse), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadCourseOutput>(existingCourse), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_IdIsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new UpdateCourseInput
            {
                Name = "Updated Course Name"
            };

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(null, input));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _repositoryMock.Verify(repo => repo.GetCourseByNameAsync(It.IsAny<string>()), Times.Never);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Course>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadCourseOutput>(It.IsAny<Domain.Entities.Course>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_NameIsNullOrEmpty_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var input = new UpdateCourseInput
            {
                Name = null
            };
            var existingCourse = new Domain.Entities.Course("Existing Course Name");

            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(existingCourse);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Never);
            _repositoryMock.Verify(repo => repo.GetCourseByNameAsync(It.IsAny<string>()), Times.Never);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Course>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadCourseOutput>(It.IsAny<Domain.Entities.Course>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_CourseNotFound_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var input = new UpdateCourseInput
            {
                Name = "Updated Course Name"
            };

            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Domain.Entities.Course)null);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _repositoryMock.Verify(repo => repo.GetCourseByNameAsync(It.IsAny<string>()), Times.Never);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Course>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadCourseOutput>(It.IsAny<Domain.Entities.Course>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_CourseIsDeleted_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var input = new UpdateCourseInput
            {
                Name = "Updated Course Name"
            };
            var existingCourse = new Domain.Entities.Course("Existing Course Name");
            existingCourse.DeactivateEntity();

            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(existingCourse);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _repositoryMock.Verify(repo => repo.GetCourseByNameAsync(It.IsAny<string>()), Times.Never);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Course>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadCourseOutput>(It.IsAny<Domain.Entities.Course>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_CourseWithSameNameExists_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var input = new UpdateCourseInput
            {
                Name = "Updated Course Name"
            };
            var existingCourse = new Domain.Entities.Course("Existing Course Name");
            var existingCourseWithSameName = new Domain.Entities.Course("Updated Course Name");

            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(existingCourse);
            _repositoryMock.Setup(repo => repo.GetCourseByNameAsync(input.Name)).ReturnsAsync(existingCourseWithSameName);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _repositoryMock.Verify(repo => repo.GetCourseByNameAsync(input.Name), Times.Once);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Course>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadCourseOutput>(It.IsAny<Domain.Entities.Course>()), Times.Never);
        }
    }
}
