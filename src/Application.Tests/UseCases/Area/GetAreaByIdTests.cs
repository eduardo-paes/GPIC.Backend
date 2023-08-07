using AutoMapper;
using Application.Ports.Area;
using Domain.Interfaces.Repositories;
using Application.Validation;
using Moq;
using NUnit.Framework;
using Application.Interfaces.UseCases.Area;
using Application.UseCases.Area;

namespace Domain.Tests.UseCases.Area
{
    [TestFixture]
    public class GetAreaByIdTests
    {
        private IGetAreaById _getAreaById;
        private Mock<IAreaRepository> _areaRepositoryMock;
        private Mock<IMapper> _mapperMock;

        public static Entities.Area MockValidArea() => new(Guid.NewGuid(), "ABC", "Area Name");

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

            _areaRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(areaEntity);
            _mapperMock.Setup(m => m.Map<DetailedReadAreaOutput>(areaEntity)).Returns(detailedOutput);

            // Act
            var result = await _getAreaById.ExecuteAsync(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(detailedOutput, result);
            _areaRepositoryMock.Verify(r => r.GetByIdAsync(id), Times.Once);
            _mapperMock.Verify(m => m.Map<DetailedReadAreaOutput>(areaEntity), Times.Once);
        }

        [Test]
        public void Execute_WithNullId_ShouldThrowException()
        {
            // Arrange
            Guid? id = null;

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await _getAreaById.ExecuteAsync(id));
            _areaRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid?>()), Times.Never);
            _mapperMock.Verify(m => m.Map<DetailedReadAreaOutput>(It.IsAny<Domain.Entities.Area>()), Times.Never);
        }
    }
}
