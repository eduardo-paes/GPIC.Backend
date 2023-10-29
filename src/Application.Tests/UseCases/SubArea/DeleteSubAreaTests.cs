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
    public class DeleteSubAreaTests
    {
        private readonly Mock<ISubAreaRepository> _subAreaRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IDeleteSubArea CreateUseCase() => new DeleteSubArea(_subAreaRepositoryMock.Object, _mapperMock.Object);
        private static Domain.Entities.SubArea MockValidSubArea() => new(Guid.NewGuid(), "ABC", "SubArea Name");

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadSubAreaOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var subAreaToDelete = MockValidSubArea();
            var subAreaId = Guid.NewGuid();

            _subAreaRepositoryMock.Setup(repo => repo.DeleteAsync(subAreaId)).ReturnsAsync(subAreaToDelete);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadSubAreaOutput>(It.IsAny<Domain.Entities.SubArea>()))
                .Returns(new DetailedReadSubAreaOutput());

            // Act
            var result = await useCase.ExecuteAsync(subAreaId);

            // Assert
            Assert.NotNull(result);
            _subAreaRepositoryMock.Verify(repo => repo.DeleteAsync(subAreaId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadSubAreaOutput>(It.IsAny<Domain.Entities.SubArea>()), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_IdIsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(null));
            _subAreaRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Guid>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadSubAreaOutput>(It.IsAny<Domain.Entities.SubArea>()), Times.Never);
        }
    }
}
