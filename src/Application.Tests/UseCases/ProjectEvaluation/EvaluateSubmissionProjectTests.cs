using Xunit;
using Moq;
using AutoMapper;
using System;
using System.Collections.Generic;
using Application.UseCases.ProjectEvaluation;
using Application.Ports.Project;
using Application.Ports.ProjectEvaluation;
using Application.Validation;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Application.Tests.Mocks;
using Application.Interfaces.UseCases.ProjectEvaluation;
using Application.Ports.ProjectActivity;
using Domain.Entities;

namespace Application.Tests.UseCases.ProjectEvaluation
{
    public class EvaluateSubmissionProjectTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<ITokenAuthenticationService> _tokenAuthenticationServiceMock = new();
        private readonly Mock<IProjectActivityRepository> _projectActivityRepositoryMock = new();
        private readonly Mock<IActivityTypeRepository> _activityTypeRepositoryMock = new();
        private readonly Mock<IEmailService> _emailServiceMock = new();
        private readonly Mock<IProjectEvaluationRepository> _projectEvaluationRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IEvaluateSubmissionProject CreateUseCase() => new EvaluateSubmissionProject(
            _mapperMock.Object,
            _projectRepositoryMock.Object,
            _tokenAuthenticationServiceMock.Object,
            _projectActivityRepositoryMock.Object,
            _activityTypeRepositoryMock.Object,
            _emailServiceMock.Object,
            _projectEvaluationRepositoryMock.Object
        );

        public static EvaluateSubmissionProjectInput GetValidInput()
        {
            return new EvaluateSubmissionProjectInput
            {
                ProjectId = Guid.NewGuid(),
                IsProductivityFellow = true,
                SubmissionEvaluationStatus = 1,
                SubmissionEvaluationDescription = "This is a valid description.",
                Activities = new List<EvaluateProjectActivityInput>
                {
                    new EvaluateProjectActivityInput
                    {
                        ActivityId = Guid.NewGuid(),
                        FoundActivities = 3
                    },
                },
                Qualification = 1,
                ProjectProposalObjectives = 1,
                AcademicScientificProductionCoherence = 2,
                ProposalMethodologyAdaptation = 3,
                EffectiveContributionToResearch = 4
            };
        }

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadProjectOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var userClaims = ClaimsMock.MockValidClaims();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            project.Notice.EvaluationStartDate = DateTime.Now.AddDays(-1);
            project.Notice.EvaluationEndDate = DateTime.Now.AddDays(1);
            project.Status = EProjectStatus.Submitted;
            var activityTypes = new List<Domain.Entities.ActivityType>() { ActivityTypeMock.MockValidActivityType() };
            var projectActivities = new List<Domain.Entities.ProjectActivity>() { ProjectActivityMock.MockValidProjectActivity() };

            // Setup Mocks as needed
            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectEvaluationRepositoryMock.Setup(repo => repo.GetByProjectIdAsync(input.ProjectId)).ReturnsAsync((Domain.Entities.ProjectEvaluation)null);
            _projectRepositoryMock.Setup<Task<Domain.Entities.Project>>(repo => repo.GetByIdAsync(input.ProjectId)).ReturnsAsync(project);
            _activityTypeRepositoryMock.Setup(repo => repo.GetByNoticeIdAsync(It.IsAny<Guid>())).ReturnsAsync(activityTypes);
            _projectActivityRepositoryMock.Setup(repo => repo.GetByProjectIdAsync(It.IsAny<Guid>())).ReturnsAsync(projectActivities);
            _projectActivityRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.ProjectActivity>())).ReturnsAsync(ProjectActivityMock.MockValidProjectActivity());
            _projectEvaluationRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.ProjectEvaluation>())).ReturnsAsync(ProjectEvaluationMock.MockValidProjectEvaluation());
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadProjectOutput>(It.IsAny<Domain.Entities.Project>())).Returns(new DetailedReadProjectOutput());

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ExecuteAsync_UserNotEvaluator_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();

            // Set user claims to a non-evaluator role
            var userClaims = ClaimsMock.MockValidClaims();
            userClaims.Values.FirstOrDefault().Role = ERole.STUDENT;

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
        }

        [Fact]
        public async Task ExecuteAsync_ProjectAlreadyEvaluated_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var projectEvaluation = ProjectEvaluationMock.MockValidProjectEvaluation();
            var userClaims = ClaimsMock.MockValidClaims();

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectEvaluationRepositoryMock.Setup(repo => repo.GetByProjectIdAsync(input.ProjectId)).ReturnsAsync(projectEvaluation);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
        }
    }
}
