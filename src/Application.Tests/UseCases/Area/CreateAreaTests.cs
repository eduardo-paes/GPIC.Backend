using AutoMapper;
using Application.Ports.Area;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Area;
using Application.Validation;
using Moq;
using NUnit.Framework;
using Application.UseCases.Area;

namespace Domain.Tests.UseCases.Area
{
    [TestFixture]
    public class CreateAreaTests
    {
        private ICreateArea _createArea;
        private Mock<IAreaRepository> _areaRepositoryMock;
        private Mock<IMainAreaRepository> _mainAreaRepositoryMock;
        private Mock<IMapper> _mapperMock;

        public static Entities.Area MockValidArea() => new(Guid.NewGuid(), "ABC", "Area Name");
        private static Entities.MainArea MockValidMainArea() => new(Guid.NewGuid(), "ABC", "Main Area Name");

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
                Name = "Area Name",
                Code = "areaCode0",
                MainAreaId = Guid.NewGuid(),
            };

            var areaEntity = MockValidArea();
            var detailedOutput = new DetailedReadAreaOutput();

            _areaRepositoryMock.Setup(r => r.GetByCodeAsync(input.Code)).ReturnsAsync((Entities.Area)null);
            _mainAreaRepositoryMock.Setup(r => r.GetByIdAsync(input.MainAreaId)).ReturnsAsync(MockValidMainArea());
            _areaRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<Entities.Area>())).ReturnsAsync(areaEntity);
            _mapperMock.Setup(m => m.Map<DetailedReadAreaOutput>(areaEntity)).Returns(detailedOutput);

            // Act
            var result = await _createArea.ExecuteAsync(input);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(detailedOutput, result);
            _areaRepositoryMock.Verify(r => r.GetByCodeAsync(input.Code), Times.Once);
            _mainAreaRepositoryMock.Verify(r => r.GetByIdAsync(input.MainAreaId), Times.Once);
            _areaRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Domain.Entities.Area>()), Times.Once);
            _mapperMock.Verify(m => m.Map<DetailedReadAreaOutput>(areaEntity), Times.Once);
        }

        [Test]
        public void Execute_WithExistingCode_ShouldThrowException()
        {
            // Arrange
            var input = new CreateAreaInput
            {
                Name = "Area Name",
                Code = "existingCode2",
                MainAreaId = Guid.NewGuid(),
            };

            var existingArea = MockValidArea();

            _areaRepositoryMock.Setup(r => r.GetByCodeAsync(input.Code)).ReturnsAsync(existingArea);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await _createArea.ExecuteAsync(input));
            _areaRepositoryMock.Verify(r => r.GetByCodeAsync(input.Code), Times.Once);
            _mainAreaRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _areaRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Domain.Entities.Area>()), Times.Never);
            _mapperMock.Verify(m => m.Map<DetailedReadAreaOutput>(It.IsAny<Domain.Entities.Area>()), Times.Never);
        }

        [Test]
        public void Execute_WithMissingMainAreaId_ShouldThrowException()
        {
            // Arrange
            var input = new CreateAreaInput
            {
                Name = "Area Name",
                Code = "areaCode1",
                MainAreaId = null,
            };

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await _createArea.ExecuteAsync(input));
            _areaRepositoryMock.Verify(r => r.GetByCodeAsync(It.IsAny<string>()), Times.Once);
            _mainAreaRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _areaRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Domain.Entities.Area>()), Times.Never);
            _mapperMock.Verify(m => m.Map<DetailedReadAreaOutput>(It.IsAny<Domain.Entities.Area>()), Times.Never);
        }

        [Test]
        public void Execute_WithInactiveMainArea_ShouldThrowException()
        {
            // Arrange
            var input = new CreateAreaInput
            {
                Name = "Area Name",
                Code = "areaCode2",
                MainAreaId = Guid.NewGuid(),
            };

            var inactiveMainArea = MockValidMainArea();
            inactiveMainArea.DeactivateEntity();
            _mainAreaRepositoryMock.Setup(r => r.GetByIdAsync(input.MainAreaId)).ReturnsAsync(inactiveMainArea);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await _createArea.ExecuteAsync(input));
            _areaRepositoryMock.Verify(r => r.GetByCodeAsync(input.Code), Times.Once);
            _mainAreaRepositoryMock.Verify(r => r.GetByIdAsync(input.MainAreaId), Times.Once);
            _areaRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Domain.Entities.Area>()), Times.Never);
            _mapperMock.Verify(m => m.Map<DetailedReadAreaOutput>(It.IsAny<Domain.Entities.Area>()), Times.Never);
        }
    }
}
