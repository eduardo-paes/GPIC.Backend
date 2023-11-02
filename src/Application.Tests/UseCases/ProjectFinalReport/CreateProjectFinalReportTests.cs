using Application.Interfaces.UseCases.ProjectFinalReport;
using Application.Ports.ProjectFinalReport;
using Application.Tests.Mocks;
using Application.UseCases.ProjectFinalReport;
using Application.Validation;
using AutoMapper;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.ProjectFinalReport
{
    public class CreateProjectFinalReportTests
    {
        private readonly Mock<IProjectFinalReportRepository> _projectReportRepositoryMock = new();
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IStorageFileService> _storageFileServiceMock = new();
        private readonly Mock<ITokenAuthenticationService> _tokenAuthenticationServiceMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private ICreateProjectFinalReport CreateUseCase() => new CreateProjectFinalReport(
            _projectReportRepositoryMock.Object,
            _projectRepositoryMock.Object,
            _storageFileServiceMock.Object,
            _tokenAuthenticationServiceMock.Object,
            _mapperMock.Object
        );

        public static CreateProjectFinalReportInput GetValidInput()
        {
            // Create and return a valid input for testing
            var input = new CreateProjectFinalReportInput
            {
                ProjectId = Guid.NewGuid(),
                ReportFile = FileMock.CreateIFormFile()
            };
            return input;
        }

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadProjectFinalReportOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();

            var userClaims = ClaimsMock.MockValidClaims();
            var userClaim = userClaims.Values.FirstOrDefault();
            var actorId = userClaims.Keys.FirstOrDefault();
            var projectFinalReport = ProjectFinalReportMock.MockValidProjectFinalReport();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            project.Status = EProjectStatus.Started;
            project.ProfessorId = actorId;

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProjectId)).ReturnsAsync(project);
            _storageFileServiceMock.Setup(service => service.UploadFileAsync(input.ReportFile, null)).ReturnsAsync("file-url");
            _projectReportRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.ProjectFinalReport>())).ReturnsAsync(projectFinalReport);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadProjectFinalReportOutput>(It.IsAny<Domain.Entities.ProjectFinalReport>())).Returns(new DetailedReadProjectFinalReportOutput());

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.NotNull(result);
            _tokenAuthenticationServiceMock.Verify(service => service.GetUserAuthenticatedClaims(), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.GetByIdAsync(input.ProjectId), Times.Once);
            _storageFileServiceMock.Verify(service => service.UploadFileAsync(input.ReportFile, null), Times.Once);
            _projectReportRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.ProjectFinalReport>()), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProjectFinalReportOutput>(It.IsAny<Domain.Entities.ProjectFinalReport>()), Times.Once);
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

            // Act & Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _tokenAuthenticationServiceMock.Verify(service => service.GetUserAuthenticatedClaims(), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.GetByIdAsync(input.ProjectId), Times.Once);
            _storageFileServiceMock.Verify(service => service.UploadFileAsync(It.IsAny<IFormFile>(), null), Times.Never);

            _projectReportRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.ProjectFinalReport>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProjectFinalReportOutput>(It.IsAny<Domain.Entities.ProjectFinalReport>()), Times.Never);
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

            // Act & Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _tokenAuthenticationServiceMock.Verify(service => service.GetUserAuthenticatedClaims(), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.GetByIdAsync(input.ProjectId), Times.Once);
            _storageFileServiceMock.Verify(service => service.UploadFileAsync(It.IsAny<IFormFile>(), null), Times.Never);
            _projectReportRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.ProjectFinalReport>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProjectFinalReportOutput>(It.IsAny<Domain.Entities.ProjectFinalReport>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_ProjectNotStarted_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();

            var userClaims = ClaimsMock.MockValidClaims();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            project.Status = EProjectStatus.Opened;
            input.ProjectId = project.Id;

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProjectId)).ReturnsAsync(project);

            // Act & Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _tokenAuthenticationServiceMock.Verify(service => service.GetUserAuthenticatedClaims(), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.GetByIdAsync(input.ProjectId), Times.Once);
            _storageFileServiceMock.Verify(service => service.UploadFileAsync(It.IsAny<IFormFile>(), null), Times.Never);
            _projectReportRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.ProjectFinalReport>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProjectFinalReportOutput>(It.IsAny<Domain.Entities.ProjectFinalReport>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_ActorNotStudentOrProfessor_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();

            var userClaims = ClaimsMock.MockValidClaims();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            project.Status = EProjectStatus.Started;
            input.ProjectId = project.Id;

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProjectId)).ReturnsAsync(project);

            // Act & Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _tokenAuthenticationServiceMock.Verify(service => service.GetUserAuthenticatedClaims(), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.GetByIdAsync(input.ProjectId), Times.Once);
            _storageFileServiceMock.Verify(service => service.UploadFileAsync(It.IsAny<IFormFile>(), null), Times.Never);
            _projectReportRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.ProjectFinalReport>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProjectFinalReportOutput>(It.IsAny<Domain.Entities.ProjectFinalReport>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_ReportOutsideDeadline_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();

            var userClaims = ClaimsMock.MockValidClaims();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            project.Status = EProjectStatus.Started;
            project.Notice!.FinalReportDeadline = DateTime.UtcNow.AddMonths(-7);
            input.ProjectId = project.Id;

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProjectId)).ReturnsAsync(project);

            // Act & Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _tokenAuthenticationServiceMock.Verify(service => service.GetUserAuthenticatedClaims(), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.GetByIdAsync(input.ProjectId), Times.Once);
            _storageFileServiceMock.Verify(service => service.UploadFileAsync(It.IsAny<IFormFile>(), null), Times.Never);
            _projectReportRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.ProjectFinalReport>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProjectFinalReportOutput>(It.IsAny<Domain.Entities.ProjectFinalReport>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_ReportFileInvalid_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            input.ReportFile = null;

            var userClaims = ClaimsMock.MockValidClaims();
            var userClaim = userClaims.Values.FirstOrDefault();
            var actorId = userClaims.Keys.FirstOrDefault();
            var projectFinalReport = ProjectFinalReportMock.MockValidProjectFinalReport();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            project.Status = EProjectStatus.Started;
            project.ProfessorId = actorId;

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProjectId)).ReturnsAsync(project);
            _storageFileServiceMock.Setup(service => service.UploadFileAsync(input.ReportFile, null)).ReturnsAsync((string)null);

            // Act & Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _tokenAuthenticationServiceMock.Verify(service => service.GetUserAuthenticatedClaims(), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.GetByIdAsync(input.ProjectId), Times.Once);
            _storageFileServiceMock.Verify(service => service.UploadFileAsync(input.ReportFile, null), Times.Never);
            _projectReportRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.ProjectFinalReport>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProjectFinalReportOutput>(It.IsAny<Domain.Entities.ProjectFinalReport>()), Times.Never);
        }
    }
}
