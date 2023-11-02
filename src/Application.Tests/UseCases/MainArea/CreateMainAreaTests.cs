using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.MainArea;
using Application.Ports.MainArea;
using Application.UseCases.MainArea;
using Moq;
using Xunit;
using Application.Validation;

namespace Application.Tests.UseCases.MainArea
{
    public class CreateMainAreaTests
    {
        private readonly Mock<IMainAreaRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private ICreateMainArea CreateUseCase() => new CreateMainArea(_repositoryMock.Object, _mapperMock.Object);

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedMainAreaOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new CreateMainAreaInput
            {
                Code = "NewCode",
                Name = "NewName"
            };
            Domain.Entities.MainArea existingMainArea = null;

            _repositoryMock.Setup(repo => repo.GetByCodeAsync(input.Code)).ReturnsAsync(existingMainArea);
            _repositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.MainArea>())).ReturnsAsync(new Domain.Entities.MainArea(input.Code, input.Name));
            _mapperMock.Setup(mapper => mapper.Map<DetailedMainAreaOutput>(It.IsAny<Domain.Entities.MainArea>())).Returns(new DetailedMainAreaOutput());

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(repo => repo.GetByCodeAsync(input.Code), Times.Once);
            _repositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.MainArea>()), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedMainAreaOutput>(It.IsAny<Domain.Entities.MainArea>()), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_CodeAlreadyExists_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new CreateMainAreaInput
            {
                Code = "ExistingCode",
                Name = "NewName"
            };
            var existingMainArea = new Domain.Entities.MainArea("ExistingCode", "ExistingName");

            _repositoryMock.Setup(repo => repo.GetByCodeAsync(input.Code)).ReturnsAsync(existingMainArea);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _repositoryMock.Verify(repo => repo.GetByCodeAsync(input.Code), Times.Once);
            _repositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.MainArea>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedMainAreaOutput>(It.IsAny<Domain.Entities.MainArea>()), Times.Never);
        }
    }
}
