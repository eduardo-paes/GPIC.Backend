using Application.Interfaces.UseCases.ProjectPartialReport;
using Application.Ports.ProjectPartialReport;
using Application.Tests.Mocks;
using Application.UseCases.ProjectPartialReport;
using Application.Validation;
using AutoMapper;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.ProjectPartialReport
{
    public class CreateProjectPartialReportTests
    {
        private readonly Mock<IProjectPartialReportRepository> _projectReportRepositoryMock = new();
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<ITokenAuthenticationService> _tokenAuthenticationServiceMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private ICreateProjectPartialReport CreateUseCase() =>
            new CreateProjectPartialReport(_projectReportRepositoryMock.Object, _projectRepositoryMock.Object, _tokenAuthenticationServiceMock.Object, _mapperMock.Object);

        private CreateProjectPartialReportInput GetValidInput()
        {
            return new CreateProjectPartialReportInput
            {
                ProjectId = Guid.NewGuid(),
                CurrentDevelopmentStage = 1,
                ScholarPerformance = 0,
                AdditionalInfo = "Additional Information",
            };
        }

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadProjectPartialReportOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var userClaims = ClaimsMock.MockValidClaims();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            project.StudentId = userClaims.Keys.FirstOrDefault();
            project.Status = EProjectStatus.Started;
            input.ProjectId = project.Id;
            var report = ProjectPartialReportMock.MockValidProjectPartialReport();
            report.UserId = userClaims.Values.FirstOrDefault().Id;

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProjectId)).ReturnsAsync(project);
            _projectReportRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.ProjectPartialReport>()))
                .ReturnsAsync(report);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadProjectPartialReportOutput>(It.IsAny<Domain.Entities.ProjectPartialReport>()))
                .Returns(new DetailedReadProjectPartialReportOutput());

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.NotNull(result);
            _projectRepositoryMock.Verify(repo => repo.GetByIdAsync(input.ProjectId), Times.Once);
            _projectReportRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.ProjectPartialReport>()), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProjectPartialReportOutput>(It.IsAny<Domain.Entities.ProjectPartialReport>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ProjectNotFound_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();

            var userClaims = ClaimsMock.MockValidClaims();

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProjectId)).ReturnsAsync((Domain.Entities.Project)null);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
        }

        [Fact]
        public async Task ExecuteAsync_ProjectDeleted_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();

            var userClaims = ClaimsMock.MockValidClaims();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            project.DeactivateEntity();
            input.ProjectId = project.Id;

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProjectId)).ReturnsAsync(project);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
        }

        [Fact]
        public async Task ExecuteAsync_ProjectNotStarted_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();

            var userClaims = ClaimsMock.MockValidClaims();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            project.StudentId = userClaims.Keys.FirstOrDefault();
            project.Status = EProjectStatus.Opened;
            input.ProjectId = project.Id;

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProjectId)).ReturnsAsync(project);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
        }

        [Fact]
        public async Task ExecuteAsync_NotAllowedActor_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();

            var userClaims = ClaimsMock.MockValidClaims();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            input.ProjectId = project.Id;
            project.Status = EProjectStatus.Started;

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProjectId)).ReturnsAsync(project);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
        }

        [Fact]
        public async Task ExecuteAsync_ReportOutsideDeadline_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();

            var userClaims = ClaimsMock.MockValidClaims();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            project.Notice.PartialReportDeadline = DateTime.UtcNow.AddMonths(12);
            project.StudentId = userClaims.Keys.FirstOrDefault();
            project.Status = EProjectStatus.Started;
            input.ProjectId = project.Id;

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProjectId)).ReturnsAsync(project);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
        }
    }
}
