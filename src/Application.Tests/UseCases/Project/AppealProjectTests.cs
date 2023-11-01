using AutoMapper;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Project;
using Application.Ports.Project;
using Application.Validation;
using Moq;
using Xunit;
using Application.Tests.Mocks;
using Application.UseCases.Project;

namespace Application.Tests.UseCases.Project
{
    public class AppealProjectTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IAppealProject CreateUseCase() => new AppealProject(
            _projectRepositoryMock.Object,
            _mapperMock.Object
        );

        [Fact]
        public async Task ExecuteAsync_ValidData_ReturnsResumedReadProjectOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var projectId = Guid.NewGuid();
            var appealDescription = "Sample appeal description";
            var project = ProjectMock.MockValidProject();
            project.Status = EProjectStatus.Rejected;

            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(projectId)).ReturnsAsync(project);
            _projectRepositoryMock.Setup(repo => repo.UpdateAsync(project)).ReturnsAsync(project);
            _mapperMock.Setup(mapper => mapper.Map<ResumedReadProjectOutput>(project)).Returns(new ResumedReadProjectOutput());

            // Act
            var result = await useCase.ExecuteAsync(projectId, appealDescription);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ResumedReadProjectOutput>(result);
            _projectRepositoryMock.Verify(repo => repo.GetByIdAsync(projectId), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.UpdateAsync(project), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<ResumedReadProjectOutput>(project), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_NoProjectFound_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var projectId = Guid.NewGuid();
            var appealDescription = "Sample appeal description";

            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(projectId)).ReturnsAsync((Domain.Entities.Project)null);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(projectId, appealDescription));
        }

        [Fact]
        public async Task ExecuteAsync_NotInAppealPhase_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var projectId = Guid.NewGuid();
            var appealDescription = "Sample appeal description";
            var project = ProjectMock.MockValidProject();
            var notice = NoticeMock.MockValidNotice();
            notice.AppealStartDate = DateTime.UtcNow.AddDays(2); // Future date
            notice.AppealEndDate = DateTime.UtcNow.AddDays(1); // Future date
            project.Notice = notice;
            project.Status = EProjectStatus.Rejected;

            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(projectId)).ReturnsAsync(project);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(projectId, appealDescription));
        }

        [Fact]
        public async Task ExecuteAsync_ProjectNotRejected_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var projectId = Guid.NewGuid();
            var appealDescription = "Sample appeal description";
            var project = ProjectMock.MockValidProject();
            project.Status = EProjectStatus.Accepted;

            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(projectId)).ReturnsAsync(project);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(projectId, appealDescription));
        }
    }
}
