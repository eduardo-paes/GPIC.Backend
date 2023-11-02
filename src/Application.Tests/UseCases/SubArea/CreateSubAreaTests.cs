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
    public class CreateSubAreaTests
    {
        private readonly Mock<ISubAreaRepository> _subAreaRepositoryMock = new();
        private readonly Mock<IAreaRepository> _areaRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private ICreateSubArea CreateUseCase() => new CreateSubArea(_subAreaRepositoryMock.Object, _areaRepositoryMock.Object, _mapperMock.Object);
        private static Domain.Entities.SubArea MockValidSubArea() => new(Guid.NewGuid(), "ABC", "SubArea Name");
        private static Domain.Entities.Area MockValidArea(CreateSubAreaInput input) => new(input.AreaId, "ABC", "Area Name");

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadSubAreaOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new CreateSubAreaInput
            {
                Code = "NewCode",
                AreaId = Guid.NewGuid()
            };
            Domain.Entities.SubArea? existingSubArea = null;
            Domain.Entities.Area area = MockValidArea(input);
            var createdSubArea = MockValidSubArea();
            createdSubArea.AreaId = input.AreaId;
            createdSubArea.Code = input.Code;

            _subAreaRepositoryMock.Setup(repo => repo.GetByCodeAsync(input.Code)).ReturnsAsync(existingSubArea);
            _areaRepositoryMock.Setup(repo => repo.GetByIdAsync(input.AreaId)).ReturnsAsync(area);
            _subAreaRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.SubArea>())).ReturnsAsync(createdSubArea);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadSubAreaOutput>(It.IsAny<Domain.Entities.SubArea>()))
                .Returns(new DetailedReadSubAreaOutput());

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.NotNull(result);
            _subAreaRepositoryMock.Verify(repo => repo.GetByCodeAsync(input.Code), Times.Once);
            _areaRepositoryMock.Verify(repo => repo.GetByIdAsync(input.AreaId), Times.Once);
            _subAreaRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.SubArea>()), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadSubAreaOutput>(It.IsAny<Domain.Entities.SubArea>()), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_SubAreaWithSameCodeExists_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new CreateSubAreaInput
            {
                Code = "ExistingCode",
                AreaId = Guid.NewGuid()
            };
            var existingSubArea = MockValidSubArea();
            existingSubArea.Code = input.Code;
            existingSubArea.AreaId = input.AreaId;
            var area = MockValidArea(input);

            _subAreaRepositoryMock.Setup(repo => repo.GetByCodeAsync(input.Code)).ReturnsAsync(existingSubArea);
            _areaRepositoryMock.Setup(repo => repo.GetByIdAsync(input.AreaId)).ReturnsAsync(area);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(input));
            _subAreaRepositoryMock.Verify(repo => repo.GetByCodeAsync(input.Code), Times.Once);
            _areaRepositoryMock.Verify(repo => repo.GetByIdAsync(input.AreaId), Times.Never);
            _subAreaRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.SubArea>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadSubAreaOutput>(It.IsAny<Domain.Entities.SubArea>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_NullAreaId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new CreateSubAreaInput
            {
                Code = "NewCode",
                AreaId = null
            };

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(input));
            _subAreaRepositoryMock.Verify(repo => repo.GetByCodeAsync(input.Code), Times.Never);
            _areaRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _subAreaRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.SubArea>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadSubAreaOutput>(It.IsAny<Domain.Entities.SubArea>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_InactiveArea_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new CreateSubAreaInput
            {
                Code = "NewCode",
                AreaId = Guid.NewGuid()
            };
            Domain.Entities.SubArea? existingSubArea = null;
            var area = MockValidArea(input);
            area.DeactivateEntity();

            _subAreaRepositoryMock.Setup(repo => repo.GetByCodeAsync(input.Code)).ReturnsAsync(existingSubArea);
            _areaRepositoryMock.Setup(repo => repo.GetByIdAsync(input.AreaId)).ReturnsAsync(area);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(() => useCase.ExecuteAsync(input));
            _subAreaRepositoryMock.Verify(repo => repo.GetByCodeAsync(input.Code), Times.Once);
            _areaRepositoryMock.Verify(repo => repo.GetByIdAsync(input.AreaId), Times.Once);
            _subAreaRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.SubArea>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadSubAreaOutput>(It.IsAny<Domain.Entities.SubArea>()), Times.Never);
        }
    }
}
