using Application.Interfaces.UseCases.Project;
using Application.Ports.Project;
using Application.Ports.ProjectActivity;
using Application.Tests.Mocks;
using Application.UseCases.Project;
using Application.Validation;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.Project
{
    public class OpenProjectTests
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

        private IOpenProject CreateUseCase() => new OpenProject(
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

        public static OpenProjectInput GetValidInput()
        {
            return new OpenProjectInput
            {
                Title = "My Project Title",
                KeyWord1 = "Keyword1",
                KeyWord2 = "Keyword2",
                KeyWord3 = "Keyword3",
                IsScholarshipCandidate = true,
                Objective = "Project Objective",
                Methodology = "Project Methodology",
                ExpectedResults = "Expected Results",
                ActivitiesExecutionSchedule = "Activity Schedule",
                Activities = new List<CreateProjectActivityInput>
                {
                    new() {
                        ActivityId = Guid.NewGuid(),
                        InformedActivities = 10
                    },
                    new() {
                        ActivityId = Guid.NewGuid(),
                        InformedActivities = 10
                    }
                },
                ProgramTypeId = Guid.NewGuid(),
                ProfessorId = Guid.NewGuid(),
                SubAreaId = Guid.NewGuid(),
                NoticeId = Guid.NewGuid(),
                StudentId = Guid.NewGuid()
            };
        }

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsResumedReadProjectOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            var notice = NoticeMock.MockValidNoticeWithId();
            var student = StudentMock.MockValidStudentWithId();
            var activityType = new List<Domain.Entities.ActivityType> { ActivityTypeMock.MockValidActivityType() };
            var projectActivity = ProjectActivityMock.MockValidProjectActivity();

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
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.NotNull(result);

            _noticeRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _subAreaRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _programTypeRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _professorRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _studentRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.GetStudentProjectsAsync(0, 1, It.IsAny<Guid>(), false), Times.Once);
            _activityTypeRepositoryMock.Verify(repo => repo.GetByNoticeIdAsync(It.IsAny<Guid>()), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Project>()), Times.Once);
            _projectActivityRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.ProjectActivity>()), Times.AtMostOnce);
            _mapperMock.Verify(mapper => mapper.Map<ResumedReadProjectOutput>(It.IsAny<Domain.Entities.Project>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ProjectOutsideRegistrationPeriod_ThrowsBusinessRuleViolation()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var notice = NoticeMock.MockValidNotice();
            notice.RegistrationStartDate = DateTime.Now.AddDays(-2);
            notice.RegistrationEndDate = DateTime.Now.AddDays(-1);

            // Setup your mocks here
            _noticeRepositoryMock.Setup(repo => repo.GetByIdAsync(input.NoticeId)).ReturnsAsync(notice);

            // Act & Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _noticeRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _subAreaRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_SubAreaNotFound_ThrowsNotFoundEntityById()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();

            // Setup your mocks here
            _noticeRepositoryMock.Setup(repo => repo.GetByIdAsync(input.NoticeId)).ReturnsAsync(project.Notice);
            _subAreaRepositoryMock.Setup(repo => repo.GetByIdAsync(input.SubAreaId)).ReturnsAsync((Domain.Entities.SubArea)null);
            // Set up other necessary mock setups

            // Act & Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _subAreaRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _programTypeRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_ProgramTypeNotFound_ThrowsNotFoundEntityById()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();

            // Setup your mocks here
            _noticeRepositoryMock.Setup(repo => repo.GetByIdAsync(input.NoticeId)).ReturnsAsync(project.Notice);
            _subAreaRepositoryMock.Setup(repo => repo.GetByIdAsync(input.SubAreaId)).ReturnsAsync(project.SubArea);
            _programTypeRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProgramTypeId)).ReturnsAsync((Domain.Entities.ProgramType)null);

            // Act & Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _programTypeRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _professorRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_ProfessorNotFound_ThrowsNotFoundEntityById()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();

            // Setup your mocks here
            _noticeRepositoryMock.Setup(repo => repo.GetByIdAsync(input.NoticeId)).ReturnsAsync(project.Notice);
            _subAreaRepositoryMock.Setup(repo => repo.GetByIdAsync(input.SubAreaId)).ReturnsAsync(project.SubArea);
            _programTypeRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProgramTypeId)).ReturnsAsync(project.ProgramType);
            _professorRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProfessorId)).ReturnsAsync((Domain.Entities.Professor)null);

            // Act & Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _professorRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _studentRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_StudentNotFound_ThrowsNotFoundEntityById()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();

            // Setup your mocks here
            _noticeRepositoryMock.Setup(repo => repo.GetByIdAsync(input.NoticeId)).ReturnsAsync(project.Notice);
            _subAreaRepositoryMock.Setup(repo => repo.GetByIdAsync(input.SubAreaId)).ReturnsAsync(project.SubArea);
            _programTypeRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProgramTypeId)).ReturnsAsync(project.ProgramType);
            _professorRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProfessorId)).ReturnsAsync(project.Professor);
            _studentRepositoryMock.Setup(repo => repo.GetByIdAsync(input.StudentId)).ReturnsAsync((Domain.Entities.Student)null);

            // Act & Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _studentRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.GetStudentProjectsAsync(0, 1, It.IsAny<Guid>(), false), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_StudentAlreadyHasProject_ThrowsBusinessRuleViolation()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            var student = StudentMock.MockValidStudentWithId();

            // Setup your mocks here
            _noticeRepositoryMock.Setup(repo => repo.GetByIdAsync(input.NoticeId)).ReturnsAsync(project.Notice);
            _subAreaRepositoryMock.Setup(repo => repo.GetByIdAsync(input.SubAreaId)).ReturnsAsync(project.SubArea);
            _programTypeRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProgramTypeId)).ReturnsAsync(project.ProgramType);
            _professorRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProfessorId)).ReturnsAsync(project.Professor);
            _studentRepositoryMock.Setup(repo => repo.GetByIdAsync(input.StudentId)).ReturnsAsync(student);
            _projectRepositoryMock.Setup(repo => repo.GetStudentProjectsAsync(0, 1, It.IsAny<Guid>(), false)).ReturnsAsync(new List<Domain.Entities.Project> { project });

            // Act & Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _studentRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.GetStudentProjectsAsync(0, 1, It.IsAny<Guid>(), false), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Project>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_ActivityTypeNotFound_ThrowsNotFoundEntityById()
        {
            // Arrange
            var useCase = CreateUseCase();
            var input = GetValidInput();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            var student = StudentMock.MockValidStudentWithId();

            // Setup your mocks here
            _noticeRepositoryMock.Setup(repo => repo.GetByIdAsync(input.NoticeId)).ReturnsAsync(project.Notice);
            _subAreaRepositoryMock.Setup(repo => repo.GetByIdAsync(input.SubAreaId)).ReturnsAsync(project.SubArea);
            _programTypeRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProgramTypeId)).ReturnsAsync(project.ProgramType);
            _professorRepositoryMock.Setup(repo => repo.GetByIdAsync(input.ProfessorId)).ReturnsAsync(project.Professor);
            _studentRepositoryMock.Setup(repo => repo.GetByIdAsync(input.StudentId)).ReturnsAsync(student);
            _projectRepositoryMock.Setup(repo => repo.GetStudentProjectsAsync(0, 1, It.IsAny<Guid>(), false)).ReturnsAsync(new List<Domain.Entities.Project>());
            _activityTypeRepositoryMock.Setup(repo => repo.GetByNoticeIdAsync(It.IsAny<Guid?>())).ReturnsAsync(new List<Domain.Entities.ActivityType>());

            // Act & Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _activityTypeRepositoryMock.Verify(repo => repo.GetByNoticeIdAsync(It.IsAny<Guid?>()), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Project>()), Times.Never);
        }
    }
}
