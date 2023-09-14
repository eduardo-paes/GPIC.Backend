using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.MainArea;
using Application.Ports.MainArea;
using Application.UseCases.MainArea;
using Moq;
using Xunit;
using Application.Validation;

namespace Application.Tests.UseCases.MainArea
{
    public class GetMainAreaByIdTests
    {
        private readonly Mock<IMainAreaRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IGetMainAreaById CreateUseCase() => new GetMainAreaById(_repositoryMock.Object, _mapperMock.Object);

        [Fact]
        public async Task ExecuteAsync_ValidId_ReturnsDetailedMainAreaOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var mainAreaEntity = new Domain.Entities.MainArea(id, "Code", "Name");

            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(mainAreaEntity);
            _mapperMock.Setup(mapper => mapper.Map<DetailedMainAreaOutput>(mainAreaEntity)).Returns(new DetailedMainAreaOutput()); // You can create a new instance here.

            // Act
            var result = await useCase.ExecuteAsync(id);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedMainAreaOutput>(mainAreaEntity), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_NullId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid? id = null;

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedMainAreaOutput>(It.IsAny<Domain.Entities.MainArea>()), Times.Never);
        }
    }
}
