using Application.Interfaces.UseCases.Project;
using Application.Ports.Project;
using Application.Tests.Mocks;
using Application.UseCases.Project;
using Application.Validation;
using AutoMapper;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.Project
{
    public class SubmitProjectTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private ISubmitProject CreateUseCase() => new SubmitProject(_projectRepositoryMock.Object, _mapperMock.Object);

        [Fact]
        public async Task ExecuteAsync_ValidProjectId_ReturnsResumedReadProjectOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var projectId = Guid.NewGuid();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            project.Status = EProjectStatus.Opened;
            project.Student = StudentMock.MockValidStudent();

            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(projectId)).ReturnsAsync(project);
            _projectRepositoryMock.Setup(repo => repo.UpdateAsync(project)).ReturnsAsync(project);
            _mapperMock.Setup(mapper => mapper.Map<ResumedReadProjectOutput>(project)).Returns(new ResumedReadProjectOutput());

            // Act
            var result = await useCase.ExecuteAsync(projectId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(EProjectStatus.Submitted, project.Status);
            Assert.Equal(EProjectStatus.Submitted.GetDescription(), project.StatusDescription);
            Assert.NotNull(project.SubmissionDate);
            _projectRepositoryMock.Verify(repo => repo.UpdateAsync(project), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<ResumedReadProjectOutput>(project), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ProjectIdIsNull_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid? projectId = null;

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(projectId));
        }

        [Fact]
        public async Task ExecuteAsync_ProjectNotFound_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var projectId = Guid.NewGuid();

            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(projectId)).ReturnsAsync((Domain.Entities.Project)null);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(projectId));
        }

        [Fact]
        public async Task ExecuteAsync_NoticeNotInRegistrationPhase_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var projectId = Guid.NewGuid();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            project.Notice.RegistrationStartDate = DateTime.UtcNow.AddHours(1);
            project.Notice.RegistrationEndDate = DateTime.UtcNow.AddHours(2);

            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(projectId)).ReturnsAsync(project);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(projectId));
        }

        [Fact]
        public async Task ExecuteAsync_ProjectWithoutStudent_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var projectId = Guid.NewGuid();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            project.StudentId = null;

            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(projectId)).ReturnsAsync(project);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(projectId));
        }

        [Fact]
        public async Task ExecuteAsync_ProjectNotOpened_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var projectId = Guid.NewGuid();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            project.Status = EProjectStatus.Submitted;
            project.Student = StudentMock.MockValidStudent();

            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(projectId)).ReturnsAsync(project);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(projectId));
        }
    }
}
