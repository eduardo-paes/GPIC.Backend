using Application.Ports.Campus;
using Application.UseCases.Campus;
using Application.Validation;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.Campus
{
    public class DeleteCampusTests
    {
        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadCampusOutput()
        {
            // Arrange
            var repositoryMock = new Mock<ICampusRepository>();
            var mapperMock = new Mock<IMapper>();
            var useCase = new DeleteCampus(repositoryMock.Object, mapperMock.Object);
            var campusId = Guid.NewGuid();

            var deletedCampus = new Domain.Entities.Campus("Deleted Campus");
            repositoryMock.Setup(repo => repo.DeleteAsync(campusId)).ReturnsAsync(deletedCampus);

            var detailedReadCampusOutput = new DetailedReadCampusOutput();
            mapperMock.Setup(mapper => mapper.Map<DetailedReadCampusOutput>(deletedCampus)).Returns(detailedReadCampusOutput);

            // Act
            var result = await useCase.ExecuteAsync(campusId);

            // Assert
            Assert.NotNull(result);
            Assert.Same(detailedReadCampusOutput, result);
            repositoryMock.Verify(repo => repo.DeleteAsync(campusId), Times.Once);
            mapperMock.Verify(mapper => mapper.Map<DetailedReadCampusOutput>(deletedCampus), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_InvalidInput_ThrowsUseCaseException()
        {
            // Arrange
            var repositoryMock = new Mock<ICampusRepository>();
            var mapperMock = new Mock<IMapper>();
            var useCase = new DeleteCampus(repositoryMock.Object, mapperMock.Object);
            Guid? campusId = null;

            // Act & Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(campusId));
            repositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Guid>()), Times.Never);
            mapperMock.Verify(mapper => mapper.Map<DetailedReadCampusOutput>(It.IsAny<Domain.Entities.Campus>()), Times.Never);
        }
    }
}
