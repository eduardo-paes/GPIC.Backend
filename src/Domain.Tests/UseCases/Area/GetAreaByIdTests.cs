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
    public class GetAreaByIdTests
    {
        private IGetAreaById _getAreaById;
        private Mock<IAreaRepository> _areaRepositoryMock;
        private Mock<IMapper> _mapperMock;

        public Domain.Entities.Area MockValidArea() => new Domain.Entities.Area(Guid.NewGuid(), "ABC", "Area Name");

        [SetUp]
        public void Setup()
        {
            _areaRepositoryMock = new Mock<IAreaRepository>();
            _mapperMock = new Mock<IMapper>();

            _getAreaById = new GetAreaById(
                _areaRepositoryMock.Object,
                _mapperMock.Object
            );
        }

        [Test]
        public async Task Execute_WithValidId_ShouldReturnDetailedReadAreaOutput()
        {
            // Arrange
            var id = Guid.NewGuid();
            var areaEntity = MockValidArea();
            var detailedOutput = new DetailedReadAreaOutput();

            _areaRepositoryMock.Setup(r => r.GetById(id)).ReturnsAsync(areaEntity);
            _mapperMock.Setup(m => m.Map<DetailedReadAreaOutput>(areaEntity)).Returns(detailedOutput);

            // Act
            var result = await _getAreaById.ExecuteAsync(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(detailedOutput, result);
            _areaRepositoryMock.Verify(r => r.GetById(id), Times.Once);
            _mapperMock.Verify(m => m.Map<DetailedReadAreaOutput>(areaEntity), Times.Once);
        }

        [Test]
        public void Execute_WithNullId_ShouldThrowException()
        {
            // Arrange
            Guid? id = null;

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await _getAreaById.ExecuteAsync(id));
            _areaRepositoryMock.Verify(r => r.GetById(It.IsAny<Guid?>()), Times.Never);
            _mapperMock.Verify(m => m.Map<DetailedReadAreaOutput>(It.IsAny<Domain.Entities.Area>()), Times.Never);
        }
    }
}
