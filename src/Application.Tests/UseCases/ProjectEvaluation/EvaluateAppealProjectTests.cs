using Application.Interfaces.UseCases.ProjectEvaluation;
using Application.Ports.Project;
using Application.Ports.ProjectEvaluation;
using Application.Tests.Mocks;
using Application.Validation;
using AutoMapper;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.ProjectEvaluation
{
    public class EvaluateAppealProjectTests
    {
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IEmailService> _emailServiceMock = new();
        private readonly Mock<ITokenAuthenticationService> _tokenAuthenticationServiceMock = new();
        private readonly Mock<IProjectEvaluationRepository> _projectEvaluationRepositoryMock = new();

        private IEvaluateAppealProject CreateUseCase()
        {
            return new Application.UseCases.ProjectEvaluation.EvaluateAppealProject(
                _mapperMock.Object,
                _projectRepositoryMock.Object,
                _emailServiceMock.Object,
                _tokenAuthenticationServiceMock.Object,
                _projectEvaluationRepositoryMock.Object
            );
        }

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadProjectOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new EvaluateAppealProjectInput
            {
                AppealEvaluationStatus = 4, // Accepted
                AppealEvaluationDescription = "Accepted appeal description",
                ProjectId = Guid.NewGuid(),
            };

            var userClaims = ClaimsMock.MockValidClaims();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            project.Status = EProjectStatus.Evaluation;
            project.Notice.AppealStartDate = DateTime.Now.AddDays(-1);
            project.Notice.AppealEndDate = DateTime.Now.AddDays(1);

            var projectEvaluation = ProjectEvaluationMock.MockValidProjectEvaluation();
            projectEvaluation.Project = project;

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectEvaluationRepositoryMock.Setup(repo => repo.GetByProjectIdAsync(input.ProjectId)).ReturnsAsync(projectEvaluation);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProjectId)).ReturnsAsync(project);
            _emailServiceMock.Setup(service => service.SendProjectNotificationEmailAsync(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            _projectEvaluationRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.ProjectEvaluation>())).ReturnsAsync(projectEvaluation);
            _projectRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Project>())).ReturnsAsync(project);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadProjectOutput>(project)).Returns(new DetailedReadProjectOutput());

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.NotNull(result);
            _tokenAuthenticationServiceMock.Verify(service => service.GetUserAuthenticatedClaims(), Times.Once);
            _projectEvaluationRepositoryMock.Verify(repo => repo.GetByProjectIdAsync(input.ProjectId), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.GetByIdAsync(input.ProjectId), Times.Once);
            _emailServiceMock.Verify(service => service.SendProjectNotificationEmailAsync(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _projectEvaluationRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.ProjectEvaluation>()), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Project>()), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProjectOutput>(project), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_UserNotEvaluator_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new EvaluateAppealProjectInput
            {
                AppealEvaluationStatus = 4, // Accepted
                AppealEvaluationDescription = "Accepted appeal description",
                ProjectId = Guid.NewGuid(),
            };

            var userClaims = ClaimsMock.MockValidClaims();
            userClaims.Values.First().Role = ERole.STUDENT;

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);

            // Act & Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
        }

        [Fact]
        public async Task ExecuteAsync_AppealStatusNotInformed_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = new EvaluateAppealProjectInput
            {
                AppealEvaluationStatus = null,
                AppealEvaluationDescription = "Appeal description",
                ProjectId = Guid.NewGuid(),
            };

            var userClaims = ClaimsMock.MockValidClaims();

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);

            // Act & Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
        }
    }
}
