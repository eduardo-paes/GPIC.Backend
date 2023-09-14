using Application.Ports.Campus;
using Application.UseCases.Campus;
using Application.Validation;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.Campus
{
    public class GetCampusByIdTests
    {
        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadCampusOutput()
        {
            // Arrange
            var repositoryMock = new Mock<ICampusRepository>();
            var mapperMock = new Mock<IMapper>();

            var useCase = new GetCampusById(repositoryMock.Object, mapperMock.Object);

            var campusId = Guid.NewGuid();

            // Simule o retorno do repositório quando GetByIdAsync é chamado
            var campus = new Domain.Entities.Campus("Campus Name");
            repositoryMock.Setup(repo => repo.GetByIdAsync(campusId)).ReturnsAsync(campus);

            // Simule o mapeamento do AutoMapper
            var detailedReadCampusOutput = new DetailedReadCampusOutput(); // Você pode preencher com dados relevantes
            mapperMock.Setup(mapper => mapper.Map<DetailedReadCampusOutput>(campus)).Returns(detailedReadCampusOutput);

            // Act
            var result = await useCase.ExecuteAsync(campusId);

            // Assert
            Assert.NotNull(result);
            Assert.Same(detailedReadCampusOutput, result);
            repositoryMock.Verify(repo => repo.GetByIdAsync(campusId), Times.Once);
            mapperMock.Verify(mapper => mapper.Map<DetailedReadCampusOutput>(campus), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_InvalidInput_ThrowsUseCaseException()
        {
            // Arrange
            var repositoryMock = new Mock<ICampusRepository>();
            var mapperMock = new Mock<IMapper>();

            var useCase = new GetCampusById(repositoryMock.Object, mapperMock.Object);

            Guid? campusId = null;

            // Act & Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(campusId));
            repositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid?>()), Times.Never);
            mapperMock.Verify(mapper => mapper.Map<DetailedReadCampusOutput>(It.IsAny<Domain.Entities.Campus>()), Times.Never);
        }
    }
}
