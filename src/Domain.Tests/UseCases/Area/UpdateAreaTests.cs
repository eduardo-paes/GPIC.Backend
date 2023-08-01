using System;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Ports.Area;
using Domain.UseCases;
using Domain.UseCases.Interfaces.Repositories;
using Domain.UseCases.Interfaces.UseCases;
using Domain.Validation;
using Moq;
using NUnit.Framework;

namespace Domain.Tests.UseCases.Area
{
    [TestFixture]
    public class UpdateAreaTests
    {
        private IUpdateArea _updateArea;
        private Mock<IAreaRepository> _areaRepositoryMock;
        private Mock<IMapper> _mapperMock;

        public Domain.Entities.Area MockValidArea() => new Domain.Entities.Area(Guid.NewGuid(), "ABC", "Area Name");

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

            _areaRepositoryMock.Setup(r => r.GetById(id)).ReturnsAsync(areaEntity);
            _areaRepositoryMock.Setup(r => r.Update(It.IsAny<Domain.Entities.Area>())).ReturnsAsync(areaEntity);
            _mapperMock.Setup(m => m.Map<DetailedReadAreaOutput>(areaEntity)).Returns(detailedOutput);

            // Act
            var result = await _updateArea.Execute(id, input);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(detailedOutput, result);
            _areaRepositoryMock.Verify(r => r.GetById(id), Times.Once);
            _areaRepositoryMock.Verify(r => r.Update(areaEntity), Times.Once);
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
            Assert.ThrowsAsync<UseCaseException>(async () => await _updateArea.Execute(id, input));
            _areaRepositoryMock.Verify(r => r.GetById(It.IsAny<Guid?>()), Times.Never);
            _areaRepositoryMock.Verify(r => r.Update(It.IsAny<Domain.Entities.Area>()), Times.Never);
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

            _areaRepositoryMock.Setup(r => r.GetById(id)).ReturnsAsync((Domain.Entities.Area)null);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await _updateArea.Execute(id, input));
            _areaRepositoryMock.Verify(r => r.GetById(id), Times.Once);
            _areaRepositoryMock.Verify(r => r.Update(It.IsAny<Domain.Entities.Area>()), Times.Never);
            _mapperMock.Verify(m => m.Map<DetailedReadAreaOutput>(It.IsAny<Domain.Entities.Area>()), Times.Never);
        }
    }
}