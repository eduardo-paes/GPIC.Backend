using Application.Interfaces.UseCases.AssistanceType;
using Application.Ports.AssistanceType;
using Application.UseCases.AssistanceType;
using Application.Validation;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.AssistanceType
{
    public class CreateAssistanceTypeTests
    {
        private readonly Mock<IAssistanceTypeRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private ICreateAssistanceType CreateUseCase() => new CreateAssistanceType(_repositoryMock.Object, _mapperMock.Object);

        private Domain.Entities.AssistanceType MockValidAssistanceType() => new("AssistanceTypeName", "AssistanceTypeDescription");
        private CreateAssistanceTypeInput MockValidAssistanceTypeInput() => new()
        {
            Name = "AssistanceTypeName",
            Description = "AssistanceTypeDescription"
        };

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadAssistanceTypeOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = MockValidAssistanceTypeInput();

            _repositoryMock.Setup(repo => repo.GetAssistanceTypeByNameAsync(input.Name)).ReturnsAsync((Domain.Entities.AssistanceType)null);
            _mapperMock.Setup(mapper => mapper.Map<Domain.Entities.AssistanceType>(input)).Returns(MockValidAssistanceType());
            _repositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.AssistanceType>())).ReturnsAsync(MockValidAssistanceType());
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadAssistanceTypeOutput>(It.IsAny<Domain.Entities.AssistanceType>())).Returns(new DetailedReadAssistanceTypeOutput());

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(repo => repo.GetAssistanceTypeByNameAsync(input.Name), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<Domain.Entities.AssistanceType>(input), Times.Once);
            _repositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.AssistanceType>()), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadAssistanceTypeOutput>(It.IsAny<Domain.Entities.AssistanceType>()), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_NameIsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new CreateAssistanceTypeInput
            {
                Name = null
            };

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _repositoryMock.Verify(repo => repo.GetAssistanceTypeByNameAsync(It.IsAny<string>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<Domain.Entities.AssistanceType>(It.IsAny<CreateAssistanceTypeInput>()), Times.Never);
            _repositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.AssistanceType>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadAssistanceTypeOutput>(It.IsAny<Domain.Entities.AssistanceType>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_AssistanceTypeWithNameExists_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = MockValidAssistanceTypeInput();

            _repositoryMock.Setup(repo => repo.GetAssistanceTypeByNameAsync(input.Name)).ReturnsAsync(MockValidAssistanceType());

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _repositoryMock.Verify(repo => repo.GetAssistanceTypeByNameAsync(input.Name), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<Domain.Entities.AssistanceType>(It.IsAny<CreateAssistanceTypeInput>()), Times.Never);
            _repositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.AssistanceType>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadAssistanceTypeOutput>(It.IsAny<Domain.Entities.AssistanceType>()), Times.Never);
        }
    }
}
