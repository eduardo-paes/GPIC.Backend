using Application.Interfaces.UseCases.AssistanceType;
using Application.Ports.AssistanceType;
using Application.UseCases.AssistanceType;
using Application.Validation;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.UseCases.AssistanceType
{
    public class GetAssistanceTypeByIdTests
    {
        private readonly Mock<IAssistanceTypeRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IGetAssistanceTypeById CreateUseCase()
        {
            return new GetAssistanceTypeById(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadAssistanceTypeOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var assistanceType = new Domain.Entities.AssistanceType(id, "AssistanceTypeName", "AssistanceTypeDescription");

            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(assistanceType);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadAssistanceTypeOutput>(assistanceType)).Returns(new DetailedReadAssistanceTypeOutput());

            // Act
            var result = await useCase.ExecuteAsync(id);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadAssistanceTypeOutput>(assistanceType), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_IdIsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid? id = null;

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id));
            _repositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadAssistanceTypeOutput>(It.IsAny<Domain.Entities.AssistanceType>()), Times.Never);
        }
    }
}
