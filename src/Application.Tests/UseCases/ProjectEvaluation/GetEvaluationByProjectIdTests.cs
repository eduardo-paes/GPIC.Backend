using System;
using System.Threading.Tasks;
using Application.Interfaces.UseCases.ProjectEvaluation;
using Application.Ports.ProjectEvaluation;
using Application.Tests.Mocks;
using Application.Validation;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.ProjectEvaluation
{
    public class GetEvaluationByProjectIdTests
    {
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly Mock<IProjectEvaluationRepository> _repositoryMock = new Mock<IProjectEvaluationRepository>();

        private IGetEvaluationByProjectId CreateUseCase() => new Application.UseCases.ProjectEvaluation.GetEvaluationByProjectId(
            _mapperMock.Object,
            _repositoryMock.Object
        );

        [Fact]
        public async Task ExecuteAsync_ValidProjectId_ReturnsDetailedReadProjectEvaluationOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var projectId = Guid.NewGuid();
            var projectEvaluationEntity = ProjectEvaluationMock.MockValidProjectEvaluation();

            _repositoryMock.Setup(repo => repo.GetByProjectIdAsync(projectId)).ReturnsAsync(projectEvaluationEntity);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadProjectEvaluationOutput>(projectEvaluationEntity)).Returns(new DetailedReadProjectEvaluationOutput());

            // Act
            var result = await useCase.ExecuteAsync(projectId);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(repo => repo.GetByProjectIdAsync(projectId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProjectEvaluationOutput>(projectEvaluationEntity), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_NullProjectId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid? projectId = null;

            // Act & Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(projectId));
        }

        [Fact]
        public async Task ExecuteAsync_ProjectNotFound_ReturnsNull()
        {
            // Arrange
            var useCase = CreateUseCase();
            var projectId = Guid.NewGuid();

            _repositoryMock.Setup(repo => repo.GetByProjectIdAsync(projectId)).ReturnsAsync((Domain.Entities.ProjectEvaluation)null);

            // Act
            var result = await useCase.ExecuteAsync(projectId);

            // Assert
            Assert.Null(result);
            _repositoryMock.Verify(repo => repo.GetByProjectIdAsync(projectId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProjectEvaluationOutput>(It.IsAny<Domain.Entities.ProjectEvaluation>()), Times.Once);
        }
    }
}
