using AutoMapper;
using Domain.Contracts.Area;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases;
using Domain.UseCases;
using Domain.Validation;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Domain.Tests.UseCases.Area
{
    [TestFixture]
    public class CreateAreaTests
    {
        private ICreateArea _createArea;
        private Mock<IAreaRepository> _areaRepositoryMock;
        private Mock<IMainAreaRepository> _mainAreaRepositoryMock;
        private Mock<IMapper> _mapperMock;

        public Domain.Entities.Area MockValidArea() => new Domain.Entities.Area(Guid.NewGuid(), "ABC", "Area Name");
        private Domain.Entities.MainArea MockValidMainArea() => new Domain.Entities.MainArea(Guid.NewGuid(), "ABC", "Main Area Name");

        [SetUp]
        public void Setup()
        {
            _areaRepositoryMock = new Mock<IAreaRepository>();
            _mainAreaRepositoryMock = new Mock<IMainAreaRepository>();
            _mapperMock = new Mock<IMapper>();

            _createArea = new CreateArea(
                _areaRepositoryMock.Object,
                _mainAreaRepositoryMock.Object,
                _mapperMock.Object
            );
        }

        [Test]
        public async Task Execute_WithValidInput_ShouldCreateArea()
        {
            // Arrange
            var input = new CreateAreaInput
            {
                Code = "areaCode0",
                MainAreaId = Guid.NewGuid(),
            };

            var areaEntity = MockValidArea();
            var detailedOutput = new DetailedReadAreaOutput();

            _areaRepositoryMock.Setup(r => r.GetByCode(input.Code)).ReturnsAsync((Domain.Entities.Area)null);
            _mainAreaRepositoryMock.Setup(r => r.GetById(input.MainAreaId)).ReturnsAsync(MockValidMainArea());
            _areaRepositoryMock.Setup(r => r.Create(It.IsAny<Domain.Entities.Area>())).ReturnsAsync(areaEntity);
            _mapperMock.Setup(m => m.Map<DetailedReadAreaOutput>(areaEntity)).Returns(detailedOutput);

            // Act
            var result = await _createArea.Execute(input);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(detailedOutput, result);
            _areaRepositoryMock.Verify(r => r.GetByCode(input.Code), Times.Once);
            _mainAreaRepositoryMock.Verify(r => r.GetById(input.MainAreaId), Times.Once);
            _areaRepositoryMock.Verify(r => r.Create(It.IsAny<Domain.Entities.Area>()), Times.Once);
            _mapperMock.Verify(m => m.Map<DetailedReadAreaOutput>(areaEntity), Times.Once);
        }

        [Test]
        public void Execute_WithExistingCode_ShouldThrowException()
        {
            // Arrange
            var input = new CreateAreaInput
            {
                Code = "existingCode2",
                MainAreaId = Guid.NewGuid(),
            };

            var existingArea = MockValidArea();

            _areaRepositoryMock.Setup(r => r.GetByCode(input.Code)).ReturnsAsync(existingArea);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await _createArea.Execute(input));
            _areaRepositoryMock.Verify(r => r.GetByCode(input.Code), Times.Once);
            _mainAreaRepositoryMock.Verify(r => r.GetById(It.IsAny<Guid>()), Times.Never);
            _areaRepositoryMock.Verify(r => r.Create(It.IsAny<Domain.Entities.Area>()), Times.Never);
            _mapperMock.Verify(m => m.Map<DetailedReadAreaOutput>(It.IsAny<Domain.Entities.Area>()), Times.Never);
        }

        [Test]
        public void Execute_WithMissingMainAreaId_ShouldThrowException()
        {
            // Arrange
            var input = new CreateAreaInput
            {
                Code = "areaCode1",
                MainAreaId = null,
            };

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await _createArea.Execute(input));
            _areaRepositoryMock.Verify(r => r.GetByCode(It.IsAny<string>()), Times.Once);
            _mainAreaRepositoryMock.Verify(r => r.GetById(It.IsAny<Guid>()), Times.Never);
            _areaRepositoryMock.Verify(r => r.Create(It.IsAny<Domain.Entities.Area>()), Times.Never);
            _mapperMock.Verify(m => m.Map<DetailedReadAreaOutput>(It.IsAny<Domain.Entities.Area>()), Times.Never);
        }

        [Test]
        public void Execute_WithInactiveMainArea_ShouldThrowException()
        {
            // Arrange
            var input = new CreateAreaInput
            {
                Code = "areaCode2",
                MainAreaId = Guid.NewGuid(),
            };

            var inactiveMainArea = MockValidMainArea();
            inactiveMainArea.DeactivateEntity();
            _mainAreaRepositoryMock.Setup(r => r.GetById(input.MainAreaId)).ReturnsAsync(inactiveMainArea);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await _createArea.Execute(input));
            _areaRepositoryMock.Verify(r => r.GetByCode(input.Code), Times.Once);
            _mainAreaRepositoryMock.Verify(r => r.GetById(input.MainAreaId), Times.Once);
            _areaRepositoryMock.Verify(r => r.Create(It.IsAny<Domain.Entities.Area>()), Times.Never);
            _mapperMock.Verify(m => m.Map<DetailedReadAreaOutput>(It.IsAny<Domain.Entities.Area>()), Times.Never);
        }
    }
}
