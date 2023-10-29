using Application.Interfaces.UseCases.SubArea;
using Application.Ports.SubArea;
using Application.UseCases.SubArea;
using Application.Validation;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.SubArea
{
    public class GetSubAreaByIdTests
    {
        private readonly Mock<ISubAreaRepository> _subAreaRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IGetSubAreaById CreateUseCase() => new GetSubAreaById(_subAreaRepositoryMock.Object, _mapperMock.Object);
        private static Domain.Entities.SubArea MockValidSubArea() => new(Guid.NewGuid(), "ABC", "SubArea Name");

        [Fact]
        public async Task ExecuteAsync_ValidId_ReturnsDetailedReadSubAreaOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var subAreaId = Guid.NewGuid();
            var subArea = MockValidSubArea();

            _subAreaRepositoryMock.Setup(repo => repo.GetByIdAsync(subAreaId)).ReturnsAsync(subArea);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadSubAreaOutput>(It.IsAny<Domain.Entities.SubArea>()))
                .Returns(new DetailedReadSubAreaOutput());

            // Act
            var result = await useCase.ExecuteAsync(subAreaId);

            // Assert
            Assert.NotNull(result);
            _subAreaRepositoryMock.Verify(repo => repo.GetByIdAsync(subAreaId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadSubAreaOutput>(It.IsAny<Domain.Entities.SubArea>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_InvalidId_ReturnsNull()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid subAreaId = Guid.NewGuid();
            _subAreaRepositoryMock.Setup(repo => repo.GetByIdAsync(subAreaId)).ReturnsAsync((Domain.Entities.SubArea)null);

            // Act
            var result = await useCase.ExecuteAsync(subAreaId);

            // Assert
            Assert.Null(result);
            _subAreaRepositoryMock.Verify(repo => repo.GetByIdAsync(subAreaId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadSubAreaOutput>(It.IsAny<Domain.Entities.SubArea>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_NullId_ReturnsNull()
        {
            // Arrange
            var useCase = CreateUseCase();

            // Act
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(null));

            // Assert
            _subAreaRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid?>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadSubAreaOutput>(It.IsAny<Domain.Entities.SubArea>()), Times.Never);
        }
    }
}
