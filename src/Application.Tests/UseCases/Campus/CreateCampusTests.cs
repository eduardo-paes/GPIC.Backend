using Application.Ports.Campus;
using Application.UseCases.Campus;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.Campus
{
    public class CreateCampusTests
    {
        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadCampusOutput()
        {
            // Arrange
            var repositoryMock = new Mock<ICampusRepository>();
            var mapperMock = new Mock<IMapper>();

            var useCase = new CreateCampus(repositoryMock.Object, mapperMock.Object);

            var input = new CreateCampusInput
            {
                Name = "Test Campus"
            };

            // Simule o retorno do repositório quando GetCampusByNameAsync é chamado
            repositoryMock.Setup(repo => repo.GetCampusByNameAsync(input.Name)).ReturnsAsync((Domain.Entities.Campus)null);

            // Simule o retorno do repositório quando CreateAsync é chamado
            var createdCampus = new Domain.Entities.Campus(input.Name);
            repositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Campus>())).ReturnsAsync(createdCampus);

            // Simule o mapeamento do AutoMapper
            var detailedReadCampusOutput = new DetailedReadCampusOutput(); // Você pode preencher com dados relevantes
            mapperMock.Setup(mapper => mapper.Map<DetailedReadCampusOutput>(createdCampus)).Returns(detailedReadCampusOutput);

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.NotNull(result);
            Assert.Same(detailedReadCampusOutput, result);
            repositoryMock.Verify(repo => repo.GetCampusByNameAsync(input.Name), Times.Once);
            repositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Campus>()), Times.Once);
            mapperMock.Verify(mapper => mapper.Map<DetailedReadCampusOutput>(createdCampus), Times.Once);
        }
    }
}
