using AutoMapper;
using Application.Ports.Area;
using Application.UseCases.Area;
using Application.Interfaces.UseCases.Area;
using Application.Validation;
using Domain.Interfaces.Repositories;
using Moq;
using NUnit.Framework;

namespace Application.Tests.UseCases.Area
{
    [TestFixture]
    public class DeleteAreaTests
    {
        private IDeleteArea _deleteArea;
        private Mock<IAreaRepository> _areaRepositoryMock;
        private Mock<IMapper> _mapperMock;

        private Guid _areaId;
        private Domain.Entities.Area _deletedArea;
        private DetailedReadAreaOutput _mappedOutput;

        [SetUp]
        public void Setup()
        {
            _areaRepositoryMock = new Mock<IAreaRepository>();
            _mapperMock = new Mock<IMapper>();

            _deleteArea = new DeleteArea(
                _areaRepositoryMock.Object,
                _mapperMock.Object
            );

            _areaId = Guid.NewGuid();
            _deletedArea = new Domain.Entities.Area(_areaId, "ABC", "Area Name");
            _mappedOutput = new DetailedReadAreaOutput();

            _areaRepositoryMock.Setup(r => r.DeleteAsync(_areaId)).ReturnsAsync(_deletedArea);
            _mapperMock.Setup(m => m.Map<DetailedReadAreaOutput>(_deletedArea)).Returns(_mappedOutput);
        }

        [Test]
        public async Task Execute_WithValidId_ShouldDeleteAreaAndReturnOutput()
        {
            // Arrange
            Guid? id = _areaId;

            // Act
            var result = await _deleteArea.ExecuteAsync(id);

            // Assert
            Assert.AreEqual(_mappedOutput, result);
            _areaRepositoryMock.Verify(r => r.DeleteAsync(_areaId), Times.Once);
            _mapperMock.Verify(m => m.Map<DetailedReadAreaOutput>(_deletedArea), Times.Once);
        }

        [Test]
        public void Execute_WithNullId_ShouldThrowArgumentNullException()
        {
            // Arrange
            Guid? id = null;

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await _deleteArea.ExecuteAsync(id));
            _areaRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Guid>()), Times.Never);
            _mapperMock.Verify(m => m.Map<DetailedReadAreaOutput>(It.IsAny<Domain.Entities.Area>()), Times.Never);
        }
    }
}
