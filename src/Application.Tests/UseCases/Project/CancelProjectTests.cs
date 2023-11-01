using Application.Interfaces.UseCases.Project;
using Application.Ports.Project;
using Application.Tests.Mocks;
using Application.Validation;
using AutoMapper;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.Project
{
    public class CancelProjectTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private ICancelProject CreateUseCase() => new Application.UseCases.Project.CancelProject(
            _projectRepositoryMock.Object,
            _mapperMock.Object
        );

        [Theory]
        [InlineData(EProjectStatus.Accepted)]
        [InlineData(EProjectStatus.DocumentAnalysis)]
        [InlineData(EProjectStatus.Evaluation)]
        [InlineData(EProjectStatus.Opened)]
        [InlineData(EProjectStatus.Pending)]
        [InlineData(EProjectStatus.Rejected)]
        [InlineData(EProjectStatus.Started)]
        [InlineData(EProjectStatus.Submitted)]
        public async Task ExecuteAsync_ValidInput_ReturnsResumedReadProjectOutput(EProjectStatus status)
        {
            // Arrange
            var useCase = CreateUseCase();
            var projectId = Guid.NewGuid();
            var observation = "Cancellation reason";

            var project = ProjectMock.MockValidProjectWithId();
            project.Status = status;

            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(projectId)).ReturnsAsync(project);
            _projectRepositoryMock.Setup(repo => repo.UpdateAsync(project)).ReturnsAsync(project);
            _mapperMock.Setup(mapper => mapper.Map<ResumedReadProjectOutput>(project)).Returns(new ResumedReadProjectOutput());

            // Act
            var result = await useCase.ExecuteAsync(projectId, observation);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(EProjectStatus.Canceled, project.Status);
            Assert.Equal(EProjectStatus.Canceled.GetDescription(), project.StatusDescription);
            Assert.Equal(observation, project.CancellationReason);
            Assert.NotNull(project.CancellationDate);
            _projectRepositoryMock.Verify(repo => repo.UpdateAsync(project), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<ResumedReadProjectOutput>(project), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<ResumedReadProjectOutput>(project), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ProjectNotFound_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var projectId = Guid.NewGuid();
            var observation = "Cancellation reason";

            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(projectId)).ReturnsAsync((Domain.Entities.Project)null);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(projectId, observation));
        }

        [Theory]
        [InlineData(EProjectStatus.Closed)]
        [InlineData(EProjectStatus.Canceled)]
        public async Task ExecuteAsync_ProjectConcluded_ThrowsUseCaseException(EProjectStatus status)
        {
            // Arrange
            var useCase = CreateUseCase();
            var observation = "Cancellation reason";

            var project = ProjectMock.MockValidProjectWithId();
            var projectId = project.Id;
            project.Status = status;

            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(projectId)).ReturnsAsync(project);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(projectId, observation));
        }
    }
}
