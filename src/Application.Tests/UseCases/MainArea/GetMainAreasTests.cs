using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.MainArea;
using Application.Ports.MainArea;
using Application.UseCases.MainArea;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.MainArea
{
    public class GetMainAreasTests
    {
        private readonly Mock<IMainAreaRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IGetMainAreas CreateUseCase() => new GetMainAreas(_repositoryMock.Object, _mapperMock.Object);

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsResumedReadMainAreaOutputQueryable()
        {
            // Arrange
            var useCase = CreateUseCase();
            var skip = 0;
            var take = 10;
            var mainAreaEntities = new List<Domain.Entities.MainArea>();

            _repositoryMock.Setup(repo => repo.GetAllAsync(skip, take)).ReturnsAsync(mainAreaEntities);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ResumedReadMainAreaOutput>>(mainAreaEntities)).Returns(new List<ResumedReadMainAreaOutput>()); // You can create a new list here.

            // Act
            var result = await useCase.ExecuteAsync(skip, take);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(repo => repo.GetAllAsync(skip, take), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<ResumedReadMainAreaOutput>>(mainAreaEntities), Times.Once);
        }
    }
}
