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
    public class DeleteCourseTests
    {
        private readonly Mock<ICourseRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IDeleteCourse CreateUseCase() => new DeleteCourse(_repositoryMock.Object, _mapperMock.Object);

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadCourseOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var deletedCourse = new Domain.Entities.Course("Deleted Course");
            _repositoryMock.Setup(repo => repo.DeleteAsync(id)).ReturnsAsync(deletedCourse);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadCourseOutput>(It.IsAny<Domain.Entities.Course>())).Returns(new DetailedReadCourseOutput());

            // Act
            var result = await useCase.ExecuteAsync(id);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(repo => repo.DeleteAsync(id), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadCourseOutput>(It.IsAny<Domain.Entities.Course>()), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_IdIsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(null));
            _repositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Guid?>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadCourseOutput>(It.IsAny<Domain.Entities.Course>()), Times.Never);
        }
    }
}
