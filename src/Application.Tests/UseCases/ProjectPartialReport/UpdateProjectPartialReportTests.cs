using Application.Interfaces.UseCases.ProjectPartialReport;
using Application.Ports.ProjectPartialReport;
using Application.Tests.Mocks;
using Application.UseCases.ProjectPartialReport;
using Application.Validation;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.ProjectPartialReport
{
    public class UpdateProjectPartialReportTests
    {
        private readonly Mock<IProjectPartialReportRepository> _projectReportRepositoryMock = new();
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<ITokenAuthenticationService> _tokenAuthenticationServiceMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IUpdateProjectPartialReport CreateUseCase() => new UpdateProjectPartialReport(
            _projectReportRepositoryMock.Object,
            _projectRepositoryMock.Object,
            _tokenAuthenticationServiceMock.Object,
            _mapperMock.Object
        );

        private static UpdateProjectPartialReportInput GetValidInput()
        {
            return new UpdateProjectPartialReportInput
            {
                CurrentDevelopmentStage = 1,
                ScholarPerformance = 2,
                AdditionalInfo = "Additional info"
            };
        }

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadProjectPartialReportOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var id = Guid.NewGuid();
            var userClaims = ClaimsMock.MockValidClaims();

            var projectReport = ProjectPartialReportMock.MockValidProjectPartialReport();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            project.Status = Domain.Entities.Enums.EProjectStatus.Started;
            project.StudentId = userClaims.Keys.FirstOrDefault();

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectReportRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(projectReport);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(projectReport.ProjectId)).ReturnsAsync(project);
            _projectReportRepositoryMock.Setup(repo => repo.UpdateAsync(projectReport)).ReturnsAsync(projectReport);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadProjectPartialReportOutput>(It.IsAny<Domain.Entities.ProjectPartialReport>()))
                .Returns(new DetailedReadProjectPartialReportOutput());

            // Act
            var result = await useCase.ExecuteAsync(id, input);

            // Assert
            Assert.NotNull(result);
            _tokenAuthenticationServiceMock.Verify(service => service.GetUserAuthenticatedClaims(), Times.Once);
            _projectReportRepositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.GetByIdAsync(projectReport.ProjectId), Times.Once);
            _projectReportRepositoryMock.Verify(repo => repo.UpdateAsync(projectReport), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProjectPartialReportOutput>(It.IsAny<Domain.Entities.ProjectPartialReport>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ReportNotFound_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var id = Guid.NewGuid();
            var userClaims = ClaimsMock.MockValidClaims();

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectReportRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Domain.Entities.ProjectPartialReport)null);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
        }

        [Fact]
        public async Task ExecuteAsync_ReportDeleted_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var id = Guid.NewGuid();
            var userClaims = ClaimsMock.MockValidClaims();

            var projectReport = ProjectPartialReportMock.MockValidProjectPartialReport();
            projectReport.DeactivateEntity();

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectReportRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(projectReport);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
        }

        [Fact]
        public async Task ExecuteAsync_ProjectNotFound_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var id = Guid.NewGuid();
            var userClaims = ClaimsMock.MockValidClaims();

            var projectReport = ProjectPartialReportMock.MockValidProjectPartialReport();

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectReportRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(projectReport);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(projectReport.ProjectId)).ReturnsAsync((Domain.Entities.Project)null);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
        }

        [Fact]
        public async Task ExecuteAsync_ProjectDeleted_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var id = Guid.NewGuid();
            var userClaims = ClaimsMock.MockValidClaims();

            var projectReport = ProjectPartialReportMock.MockValidProjectPartialReport();
            var project = ProjectMock.MockValidProject();
            project.DeactivateEntity();

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectReportRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(projectReport);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(projectReport.ProjectId)).ReturnsAsync(project);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
        }

        [Fact]
        public async Task ExecuteAsync_ProjectNotStarted_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var id = Guid.NewGuid();
            var userClaims = ClaimsMock.MockValidClaims();

            var projectReport = ProjectPartialReportMock.MockValidProjectPartialReport();
            var project = ProjectMock.MockValidProject();
            project.Status = Domain.Entities.Enums.EProjectStatus.Opened;

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectReportRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(projectReport);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(projectReport.ProjectId)).ReturnsAsync(project);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
        }

        [Fact]
        public async Task ExecuteAsync_UserNotAuthorized_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            input.CurrentDevelopmentStage = 0;
            var id = Guid.NewGuid();
            var userClaims = ClaimsMock.MockValidClaims();

            var projectReport = ProjectPartialReportMock.MockValidProjectPartialReport();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            project.Status = Domain.Entities.Enums.EProjectStatus.Started;

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectReportRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(projectReport);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(projectReport.ProjectId)).ReturnsAsync(project);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
        }

        [Fact]
        public async Task ExecuteAsync_StageOutOfRange_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            input.CurrentDevelopmentStage = 0;
            var id = Guid.NewGuid();
            var userClaims = ClaimsMock.MockValidClaims();

            var projectReport = ProjectPartialReportMock.MockValidProjectPartialReport();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            project.Status = Domain.Entities.Enums.EProjectStatus.Started;
            project.StudentId = userClaims.Keys.FirstOrDefault();
            project.Notice.PartialReportDeadline = DateTime.Now.AddMonths(7);

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectReportRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(projectReport);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(projectReport.ProjectId)).ReturnsAsync(project);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
        }

        [Fact]
        public async Task ExecuteAsync_ReportSentAfterDeadline_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            input.CurrentDevelopmentStage = 0;
            var id = Guid.NewGuid();
            var userClaims = ClaimsMock.MockValidClaims();

            var projectReport = ProjectPartialReportMock.MockValidProjectPartialReport();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            project.Status = Domain.Entities.Enums.EProjectStatus.Started;
            project.StudentId = userClaims.Keys.FirstOrDefault();
            project.Notice.PartialReportDeadline = DateTime.Now.AddMonths(-7);

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectReportRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(projectReport);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(projectReport.ProjectId)).ReturnsAsync(project);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
        }
    }
}
