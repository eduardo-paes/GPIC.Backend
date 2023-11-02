using AutoMapper;
using Application.Ports.Area;
using Domain.Interfaces.Repositories;
using Application.Validation;
using Moq;
using NUnit.Framework;
using Application.Interfaces.UseCases.Area;
using Application.UseCases.Area;

namespace Application.Tests.UseCases.Area
{
    [TestFixture]
    public class UpdateAreaTests
    {
        private IUpdateArea _updateArea;
        private Mock<IAreaRepository> _areaRepositoryMock;
        private Mock<IMapper> _mapperMock;

        public static Domain.Entities.Area MockValidArea() => new(Guid.NewGuid(), "ABC", "Area Name");

        [SetUp]
        public void Setup()
        {
            _areaRepositoryMock = new Mock<IAreaRepository>();
            _mapperMock = new Mock<IMapper>();

            _updateArea = new UpdateArea(
                _areaRepositoryMock.Object,
                _mapperMock.Object
            );
        }

        [Test]
        public async Task Execute_WithValidIdAndInput_ShouldReturnDetailedReadAreaOutput()
        {
            // Arrange
            var id = Guid.NewGuid();
            var input = new UpdateAreaInput
            {
                Name = "New Area Name",
                Code = "New Area Code",
                MainAreaId = Guid.NewGuid()
            };
            var areaEntity = MockValidArea();
            var detailedOutput = new DetailedReadAreaOutput();

            _areaRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(areaEntity);
            _areaRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Domain.Entities.Area>())).ReturnsAsync(areaEntity);
            _mapperMock.Setup(m => m.Map<DetailedReadAreaOutput>(areaEntity)).Returns(detailedOutput);

            // Act
            var result = await _updateArea.ExecuteAsync(id, input);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(detailedOutput, result);
            _areaRepositoryMock.Verify(r => r.GetByIdAsync(id), Times.Once);
            _areaRepositoryMock.Verify(r => r.UpdateAsync(areaEntity), Times.Once);
            _mapperMock.Verify(m => m.Map<DetailedReadAreaOutput>(areaEntity), Times.Once);
        }

        [Test]
        public void Execute_WithNullId_ShouldThrowException()
        {
            // Arrange
            Guid? id = null;
            var input = new UpdateAreaInput
            {
                Name = "New Area Name",
                Code = "New Area Code",
                MainAreaId = Guid.NewGuid()
            };

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await _updateArea.ExecuteAsync(id, input));
            _areaRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid?>()), Times.Never);
            _areaRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Domain.Entities.Area>()), Times.Never);
            _mapperMock.Verify(m => m.Map<DetailedReadAreaOutput>(It.IsAny<Domain.Entities.Area>()), Times.Never);
        }

        [Test]
        public void Execute_WithNonExistingId_ShouldThrowException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var input = new UpdateAreaInput
            {
                Name = "New Area Name",
                Code = "New Area Code",
                MainAreaId = Guid.NewGuid()
            };

            _areaRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Domain.Entities.Area)null);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await _updateArea.ExecuteAsync(id, input));
            _areaRepositoryMock.Verify(r => r.GetByIdAsync(id), Times.Once);
            _areaRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Domain.Entities.Area>()), Times.Never);
            _mapperMock.Verify(m => m.Map<DetailedReadAreaOutput>(It.IsAny<Domain.Entities.Area>()), Times.Never);
        }
    }
}