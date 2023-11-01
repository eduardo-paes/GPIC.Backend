using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.UseCases.Project;
using Application.Ports.Project;
using Application.Ports.ProjectActivity;
using Application.Tests.Mocks;
using Application.UseCases.Project;
using Application.Validation;
using AutoMapper;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Microsoft.VisualBasic;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.Project
{
    public class UpdateProjectTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IStudentRepository> _studentRepositoryMock = new();
        private readonly Mock<IProfessorRepository> _professorRepositoryMock = new();
        private readonly Mock<INoticeRepository> _noticeRepositoryMock = new();
        private readonly Mock<ISubAreaRepository> _subAreaRepositoryMock = new();
        private readonly Mock<IProgramTypeRepository> _programTypeRepositoryMock = new();
        private readonly Mock<IActivityTypeRepository> _activityTypeRepositoryMock = new();
        private readonly Mock<IProjectActivityRepository> _projectActivityRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IUpdateProject CreateUseCase() => new UpdateProject(
            _projectRepositoryMock.Object,
            _studentRepositoryMock.Object,
            _professorRepositoryMock.Object,
            _noticeRepositoryMock.Object,
            _subAreaRepositoryMock.Object,
            _programTypeRepositoryMock.Object,
            _activityTypeRepositoryMock.Object,
            _projectActivityRepositoryMock.Object,
            _mapperMock.Object
        );

        public static UpdateProjectInput GetValidInput()
        {
            return new UpdateProjectInput
            {
                Title = "Sample Project Title",
                KeyWord1 = "Keyword1",
                KeyWord2 = "Keyword2",
                KeyWord3 = "Keyword3",
                IsScholarshipCandidate = true,
                Objective = "Sample project objective.",
                Methodology = "Sample project methodology.",
                ExpectedResults = "Sample expected results.",
                ActivitiesExecutionSchedule = "Sample execution schedule",
                Activities = new List<UpdateProjectActivityInput>
                {
                    new UpdateProjectActivityInput
                    {
                        ActivityId = Guid.NewGuid(),
                        InformedActivities = 10
                    },
                    new UpdateProjectActivityInput
                    {
                        ActivityId = Guid.NewGuid(),
                        InformedActivities = 10
                    }
                },
                ProgramTypeId = Guid.NewGuid(),
                SubAreaId = Guid.NewGuid(),
                StudentId = Guid.NewGuid()
            };
        }

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsResumedReadProjectOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var input = GetValidInput();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            project.Status = EProjectStatus.Opened;
            var notice = NoticeMock.MockValidNoticeWithId();
            var student = StudentMock.MockValidStudentWithId();
            var activityType = new List<Domain.Entities.ActivityType> { ActivityTypeMock.MockValidActivityType() };
            var projectActivity = ProjectActivityMock.MockValidProjectActivity();

            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(project);
            _noticeRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(notice);
            _subAreaRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(project.SubArea);
            _programTypeRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(project.ProgramType);
            _professorRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(project.Professor);
            _studentRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(student);
            _projectRepositoryMock.Setup(repo => repo.GetStudentProjectsAsync(0, 1, It.IsAny<Guid>(), false)).ReturnsAsync(new List<Domain.Entities.Project>());
            _activityTypeRepositoryMock.Setup(repo => repo.GetByNoticeIdAsync(It.IsAny<Guid>())).ReturnsAsync(activityType);
            _projectRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Project>())).ReturnsAsync(project);
            _projectActivityRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.ProjectActivity>())).ReturnsAsync(projectActivity);
            _mapperMock.Setup(mapper => mapper.Map<ResumedReadProjectOutput>(It.IsAny<Domain.Entities.Project>())).Returns(new ResumedReadProjectOutput());

            // Act
            var result = await useCase.ExecuteAsync(id, input);

            // Assert
            Assert.NotNull(result);
            // Add more assertions for the returned result and repository interactions
        }

        [Fact]
        public async Task ExecuteAsync_ProjectNotFound_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var input = GetValidInput();

            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Domain.Entities.Project)null);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
            _projectRepositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _subAreaRepositoryMock.Verify(repo => repo.GetByIdAsync(input.SubAreaId), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_OutsideRegistrationPeriod_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var input = GetValidInput();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            project.Notice.RegistrationStartDate = DateTime.UtcNow.AddMinutes(30); // Registration period has ended
            project.Notice.RegistrationEndDate = DateTime.UtcNow.AddMinutes(30); // Registration period has ended

            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(project);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
            _projectRepositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _subAreaRepositoryMock.Verify(repo => repo.GetByIdAsync(input.SubAreaId), Times.Never);
        }

        [Theory]
        [InlineData(EProjectStatus.Closed)]
        [InlineData(EProjectStatus.Canceled)]
        public async Task ExecuteAsync_ProjectConcluded_ThrowsUseCaseException(EProjectStatus status)
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var input = GetValidInput();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            project.Status = status;

            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(project);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
            _projectRepositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _subAreaRepositoryMock.Verify(repo => repo.GetByIdAsync(input.SubAreaId), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_SubAreaNotFound_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var input = GetValidInput();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            var notice = NoticeMock.MockValidNoticeWithId();
            var student = StudentMock.MockValidStudentWithId();
            var activityType = new List<Domain.Entities.ActivityType> { ActivityTypeMock.MockValidActivityType() };
            var projectActivity = ProjectActivityMock.MockValidProjectActivity();

            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(project);
            _noticeRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(notice);
            _subAreaRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Domain.Entities.SubArea)null);
            _programTypeRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(project.ProgramType);
            _professorRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(project.Professor);
            _studentRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(student);
            _projectRepositoryMock.Setup(repo => repo.GetStudentProjectsAsync(0, 1, It.IsAny<Guid>(), false)).ReturnsAsync(new List<Domain.Entities.Project>());
            _activityTypeRepositoryMock.Setup(repo => repo.GetByNoticeIdAsync(It.IsAny<Guid>())).ReturnsAsync(activityType);
            _projectRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Project>())).ReturnsAsync(project);
            _projectActivityRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.ProjectActivity>())).ReturnsAsync(projectActivity);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
            _projectRepositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _subAreaRepositoryMock.Verify(repo => repo.GetByIdAsync(input.SubAreaId), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ProgramTypeNotFound_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var input = GetValidInput();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            var notice = NoticeMock.MockValidNoticeWithId();
            var student = StudentMock.MockValidStudentWithId();
            var activityType = new List<Domain.Entities.ActivityType> { ActivityTypeMock.MockValidActivityType() };
            var projectActivity = ProjectActivityMock.MockValidProjectActivity();

            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(project);
            _noticeRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(notice);
            _subAreaRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(project.SubArea);
            _programTypeRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Domain.Entities.ProgramType)null);
            _professorRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(project.Professor);
            _studentRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(student);
            _projectRepositoryMock.Setup(repo => repo.GetStudentProjectsAsync(0, 1, It.IsAny<Guid>(), false)).ReturnsAsync(new List<Domain.Entities.Project>());
            _activityTypeRepositoryMock.Setup(repo => repo.GetByNoticeIdAsync(It.IsAny<Guid>())).ReturnsAsync(activityType);
            _projectRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Project>())).ReturnsAsync(project);
            _projectActivityRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.ProjectActivity>())).ReturnsAsync(projectActivity);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
            _projectRepositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _subAreaRepositoryMock.Verify(repo => repo.GetByIdAsync(input.SubAreaId), Times.Once);
            _programTypeRepositoryMock.Verify(repo => repo.GetByIdAsync(input.ProgramTypeId), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_StudentNotFound_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var input = GetValidInput();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            var notice = NoticeMock.MockValidNoticeWithId();
            var activityType = new List<Domain.Entities.ActivityType> { ActivityTypeMock.MockValidActivityType() };
            var projectActivity = ProjectActivityMock.MockValidProjectActivity();

            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(project);
            _noticeRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(notice);
            _subAreaRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(project.SubArea);
            _programTypeRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(project.ProgramType);
            _professorRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(project.Professor);
            _studentRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Domain.Entities.Student)null);
            _projectRepositoryMock.Setup(repo => repo.GetStudentProjectsAsync(0, 1, It.IsAny<Guid>(), false)).ReturnsAsync(new List<Domain.Entities.Project>());
            _activityTypeRepositoryMock.Setup(repo => repo.GetByNoticeIdAsync(It.IsAny<Guid>())).ReturnsAsync(activityType);
            _projectRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Project>())).ReturnsAsync(project);
            _projectActivityRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.ProjectActivity>())).ReturnsAsync(projectActivity);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
            _projectRepositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _subAreaRepositoryMock.Verify(repo => repo.GetByIdAsync(input.SubAreaId), Times.Once);
            _programTypeRepositoryMock.Verify(repo => repo.GetByIdAsync(input.ProgramTypeId), Times.Once);
            _studentRepositoryMock.Verify(repo => repo.GetByIdAsync(input.StudentId.Value), Times.Once);
        }
    }
}
