using Domain.Interfaces.Repositories;
using Moq;
using Xunit;
using Application.Interfaces.UseCases.Project;
using Domain.Entities;
using Application.Tests.Mocks;
using Application.UseCases.Project;

namespace Application.Tests.UseCases.Project
{
    public class GetActivitiesByProjectIdTests
    {
        private readonly Mock<IProjectActivityRepository> _projectActivityRepositoryMock = new();

        private IGetActivitiesByProjectId CreateUseCase() => new GetActivitiesByProjectId(_projectActivityRepositoryMock.Object);

        [Fact]
        public async Task ExecuteAsync_ProjectIdIsNull_ReturnsEmptyList()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid? projectId = null;

            // Act
            var result = await useCase.ExecuteAsync(projectId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task ExecuteAsync_NoProjectActivities_ReturnsEmptyList()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid projectId = Guid.NewGuid();

            _projectActivityRepositoryMock.Setup(repo => repo.GetByProjectIdAsync(projectId)).ReturnsAsync(new List<ProjectActivity>());

            // Act
            var result = await useCase.ExecuteAsync(projectId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task ExecuteAsync_ProjectActivitiesExist_ReturnsMappedList()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid projectId = Guid.NewGuid();
            var projectActivities = new List<ProjectActivity>
            {
                ProjectActivityMock.MockValidProjectActivity()
            };

            _projectActivityRepositoryMock.Setup(repo => repo.GetByProjectIdAsync(projectId)).ReturnsAsync(projectActivities);

            // Act
            var result = await useCase.ExecuteAsync(projectId);
            var outputs = result.ToList();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
            Assert.Equal(projectActivities[0].Id, outputs[0].Id);
            Assert.Equal(projectActivities[0].ActivityId, outputs[0].ActivityId);
            Assert.Equal(projectActivities[0].ProjectId, outputs[0].ProjectId);
            Assert.Equal(projectActivities[0].InformedActivities, outputs[0].InformedActivities);
            Assert.Equal(projectActivities[0].DeletedAt, outputs[0].DeletedAt);
            Assert.Equal(projectActivities[0].FoundActivities, outputs[0].FoundActivities);
        }
    }
}
