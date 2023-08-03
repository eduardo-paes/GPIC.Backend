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

            _areaRepositoryMock.Setup(r => r.Delete(_areaId)).ReturnsAsync(_deletedArea);
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
            _areaRepositoryMock.Verify(r => r.Delete(_areaId), Times.Once);
            _mapperMock.Verify(m => m.Map<DetailedReadAreaOutput>(_deletedArea), Times.Once);
        }

        [Test]
        public void Execute_WithNullId_ShouldThrowArgumentNullException()
        {
            // Arrange
            Guid? id = null;

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await _deleteArea.ExecuteAsync(id));
            _areaRepositoryMock.Verify(r => r.Delete(It.IsAny<Guid>()), Times.Never);
            _mapperMock.Verify(m => m.Map<DetailedReadAreaOutput>(It.IsAny<Domain.Entities.Area>()), Times.Never);
        }
    }
}
