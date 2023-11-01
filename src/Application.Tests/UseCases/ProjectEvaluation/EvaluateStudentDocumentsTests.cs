using Application.Interfaces.UseCases.ProjectEvaluation;
using Application.Ports.Project;
using Application.Ports.ProjectEvaluation;
using Application.Tests.Mocks;
using Application.UseCases.ProjectEvaluation;
using Application.Validation;
using AutoMapper;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.ProjectEvaluation
{
    public class EvaluateStudentDocumentsTests
    {
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IEmailService> _emailServiceMock = new();
        private readonly Mock<ITokenAuthenticationService> _tokenAuthenticationServiceMock = new();
        private readonly Mock<IProjectEvaluationRepository> _projectEvaluationRepositoryMock = new();

        private IEvaluateStudentDocuments CreateUseCase() => new EvaluateStudentDocuments(
            _mapperMock.Object,
            _projectRepositoryMock.Object,
            _emailServiceMock.Object,
            _tokenAuthenticationServiceMock.Object,
            _projectEvaluationRepositoryMock.Object
        );

        public static EvaluateStudentDocumentsInput GetValidInput()
        {
            var input = new EvaluateStudentDocumentsInput
            {
                IsDocumentsApproved = true,
                DocumentsEvaluationDescription = "Documents approved",
                ProjectId = Guid.NewGuid(),
            };

            return input;
        }

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadProjectOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var userClaims = ClaimsMock.MockValidClaims();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            project.Status = EProjectStatus.DocumentAnalysis;
            project.Notice.SendingDocsStartDate = DateTime.UtcNow.AddDays(-1);
            project.Notice.SendingDocsEndDate = DateTime.UtcNow.AddDays(1);
            var projectEvaluation = ProjectEvaluationMock.MockValidProjectEvaluation();
            projectEvaluation.Project = project;

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProjectId)).ReturnsAsync(project);
            _projectEvaluationRepositoryMock.Setup(repo => repo.GetByProjectIdAsync(input.ProjectId)).ReturnsAsync(projectEvaluation);
            _emailServiceMock.Setup(service => service.SendProjectNotificationEmailAsync(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadProjectOutput>(It.IsAny<Domain.Entities.Project>())).Returns(new DetailedReadProjectOutput());

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.NotNull(result);
            // Add additional assertions here
        }

        [Fact]
        public async Task ExecuteAsync_UserNotEvaluator_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var userClaims = ClaimsMock.MockValidClaims();

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _tokenAuthenticationServiceMock.Verify(service => service.GetUserAuthenticatedClaims(), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_NotInformedIsDocumentsApproved_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var userClaims = ClaimsMock.MockValidClaims();

            input.IsDocumentsApproved = null;

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
        }

        [Fact]
        public async Task ExecuteAsync_DocumentsEvaluationDescriptionNotInformed_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var userClaims = ClaimsMock.MockValidClaims();

            input.DocumentsEvaluationDescription = "";

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
        }

        [Fact]
        public async Task ExecuteAsync_AvaliadorIsProfessorOrientador_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var userClaims = ClaimsMock.MockValidClaims();

            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            var projectEvaluation = ProjectEvaluationMock.MockValidProjectEvaluation();
            projectEvaluation.Project = project;
            projectEvaluation.Project.ProfessorId = userClaims.Values.First().Id;

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProjectId)).ReturnsAsync(project);
            _projectEvaluationRepositoryMock.Setup(repo => repo.GetByProjectIdAsync(input.ProjectId)).ReturnsAsync(projectEvaluation);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
        }

        [Fact]
        public async Task ExecuteAsync_ProjectNotInDocumentAnalysisPhase_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var userClaims = ClaimsMock.MockValidClaims();

            var project = ProjectMock.MockValidProject();
            var projectEvaluation = ProjectEvaluationMock.MockValidProjectEvaluation();
            project.Status = EProjectStatus.Canceled;

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProjectId)).ReturnsAsync(project);
            _projectEvaluationRepositoryMock.Setup(repo => repo.GetByProjectIdAsync(input.ProjectId)).ReturnsAsync(projectEvaluation);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
        }

        [Fact]
        public async Task ExecuteAsync_NotInDocumentAnalysisPhase_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var userClaims = ClaimsMock.MockValidClaims();

            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            var projectEvaluation = ProjectEvaluationMock.MockValidProjectEvaluation();
            project.Notice.SendingDocsStartDate = DateTime.UtcNow.AddDays(1); // Set a future date

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProjectId)).ReturnsAsync(project);
            _projectEvaluationRepositoryMock.Setup(repo => repo.GetByProjectIdAsync(input.ProjectId)).ReturnsAsync(projectEvaluation);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
        }

        [Fact]
        public async Task ExecuteAsync_ProjectNotInDatabase_ThrowsUseCaseException()
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
        public async Task ExecuteAsync_SuccessfulEvaluationApprovedStatus_ReturnsDetailedReadProjectOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var userClaims = ClaimsMock.MockValidClaims();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            project.Status = EProjectStatus.DocumentAnalysis;
            project.Notice.SendingDocsStartDate = DateTime.UtcNow.AddDays(-1);
            project.Notice.SendingDocsEndDate = DateTime.UtcNow.AddDays(1);
            var projectEvaluation = ProjectEvaluationMock.MockValidProjectEvaluation();
            projectEvaluation.Project = project;

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProjectId)).ReturnsAsync(project);
            _projectEvaluationRepositoryMock.Setup(repo => repo.GetByProjectIdAsync(input.ProjectId)).ReturnsAsync(projectEvaluation);
            _emailServiceMock.Setup(service => service.SendProjectNotificationEmailAsync(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadProjectOutput>(It.IsAny<Domain.Entities.Project>())).Returns(new DetailedReadProjectOutput());

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ExecuteAsync_SuccessfulEvaluationNotApprovedStatus_ReturnsDetailedReadProjectOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var userClaims = ClaimsMock.MockValidClaims();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            project.Status = EProjectStatus.DocumentAnalysis;
            project.Notice.SendingDocsStartDate = DateTime.UtcNow.AddDays(-1);
            project.Notice.SendingDocsEndDate = DateTime.UtcNow.AddDays(1);
            var projectEvaluation = ProjectEvaluationMock.MockValidProjectEvaluation();
            projectEvaluation.Project = project;
            input.IsDocumentsApproved = false;

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProjectId)).ReturnsAsync(project);
            _projectEvaluationRepositoryMock.Setup(repo => repo.GetByProjectIdAsync(input.ProjectId)).ReturnsAsync(projectEvaluation);
            _emailServiceMock.Setup(service => service.SendProjectNotificationEmailAsync(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadProjectOutput>(It.IsAny<Domain.Entities.Project>())).Returns(new DetailedReadProjectOutput());

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.NotNull(result);
        }
    }
}
