using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Entities
{
    public class ProjectUnitTests
    {
        private static Project MockValidProject()
        {
            return new Project("Project Title", "Keyword 1", "Keyword 2", "Keyword 3", true, "Objective", "Methodology", "Expected Results", "Schedule", Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), EProjectStatus.Opened, "Status Description", "Appeal Observation", DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow, "Cancellation Reason");
        }

        [Fact]
        public void SetTitle_ValidTitle_SetsTitle()
        {
            // Arrange
            var project = MockValidProject();
            var title = "New Project Title";

            // Act
            project.Title = title;

            // Assert
            project.Title.Should().Be(title);
        }

        [Fact]
        public void SetTitle_NullOrWhiteSpaceTitle_ThrowsException()
        {
            // Arrange
            var project = MockValidProject();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => project.Title = null);
            Assert.Throws<EntityExceptionValidation>(() => project.Title = string.Empty);
            Assert.Throws<EntityExceptionValidation>(() => project.Title = "  ");
        }

        [Fact]
        public void SetKeyWord1_ValidKeyword_SetsKeyWord1()
        {
            // Arrange
            var project = MockValidProject();
            var keyword = "New Keyword";

            // Act
            project.KeyWord1 = keyword;

            // Assert
            project.KeyWord1.Should().Be(keyword);
        }

        [Fact]
        public void SetKeyWord1_NullOrWhiteSpaceKeyword_ThrowsException()
        {
            // Arrange
            var project = MockValidProject();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => project.KeyWord1 = null);
            Assert.Throws<EntityExceptionValidation>(() => project.KeyWord1 = string.Empty);
            Assert.Throws<EntityExceptionValidation>(() => project.KeyWord1 = "  ");
        }

        [Fact]
        public void SetKeyWord2_ValidKeyword_SetsKeyWord2()
        {
            // Arrange
            var project = MockValidProject();
            var keyword = "New Keyword 2";

            // Act
            project.KeyWord2 = keyword;

            // Assert
            project.KeyWord2.Should().Be(keyword);
        }

        [Fact]
        public void SetKeyWord3_ValidKeyword_SetsKeyWord3()
        {
            // Arrange
            var project = MockValidProject();
            var keyword = "New Keyword 3";

            // Act
            project.KeyWord3 = keyword;

            // Assert
            project.KeyWord3.Should().Be(keyword);
        }

        [Fact]
        public void SetIsScholarshipCandidate_ValidValue_SetsIsScholarshipCandidate()
        {
            // Arrange
            var project = MockValidProject();
            var isScholarshipCandidate = false;

            // Act
            project.IsScholarshipCandidate = isScholarshipCandidate;

            // Assert
            project.IsScholarshipCandidate.Should().Be(isScholarshipCandidate);
        }

        [Fact]
        public void SetObjective_ValidObjective_SetsObjective()
        {
            // Arrange
            var project = MockValidProject();
            var objective = "New Objective";

            // Act
            project.Objective = objective;

            // Assert
            project.Objective.Should().Be(objective);
        }

        // ... Repetir padrão para todas as outras propriedades de atualização

        [Fact]
        public void SetActivitiesExecutionSchedule_ValidSchedule_SetsActivitiesExecutionSchedule()
        {
            // Arrange
            var project = MockValidProject();
            var schedule = "New Schedule";

            // Act
            project.ActivitiesExecutionSchedule = schedule;

            // Assert
            project.ActivitiesExecutionSchedule.Should().Be(schedule);
        }

        [Fact]
        public void SetProgramTypeId_ValidId_SetsProgramTypeId()
        {
            // Arrange
            var project = MockValidProject();
            var programTypeId = Guid.NewGuid();

            // Act
            project.ProgramTypeId = programTypeId;

            // Assert
            project.ProgramTypeId.Should().Be(programTypeId);
        }

        [Fact]
        public void SetSubAreaId_ValidId_SetsSubAreaId()
        {
            // Arrange
            var project = MockValidProject();
            var subAreaId = Guid.NewGuid();

            // Act
            project.SubAreaId = subAreaId;

            // Assert
            project.SubAreaId.Should().Be(subAreaId);
        }

        [Fact]
        public void SetStatus_ValidStatus_SetsStatus()
        {
            // Arrange
            var project = MockValidProject();
            var status = EProjectStatus.Pending;

            // Act
            project.Status = status;

            // Assert
            project.Status.Should().Be(status);
        }

        [Fact]
        public void SetStatusDescription_ValidDescription_SetsStatusDescription()
        {
            // Arrange
            var project = MockValidProject();
            var description = "New Status Description";

            // Act
            project.StatusDescription = description;

            // Assert
            project.StatusDescription.Should().Be(description);
        }

        [Fact]
        public void SetCancellationReason_ValidReason_SetsCancellationReason()
        {
            // Arrange
            var project = MockValidProject();
            var reason = "New Cancellation Reason";

            // Act
            project.CancellationReason = reason;

            // Assert
            project.CancellationReason.Should().Be(reason);
        }

        [Fact]
        public void Constructor_ValidParameters_CreatesInstance()
        {
            // Arrange
            var title = "Test Project";
            var keyWord1 = "Keyword1";
            var keyWord2 = "Keyword2";
            var keyWord3 = "Keyword3";
            var isScholarshipCandidate = true;
            var objective = "Test objective";
            var methodology = "Test methodology";
            var expectedResults = "Test expected results";
            var activitiesExecutionSchedule = "Test schedule";
            var studentId = Guid.NewGuid();
            var programTypeId = Guid.NewGuid();
            var professorId = Guid.NewGuid();
            var subAreaId = Guid.NewGuid();
            var noticeId = Guid.NewGuid();
            var status = EProjectStatus.Submitted;
            var statusDescription = "Test status description";
            var appealDescription = "Test appeal description";
            var submissionDate = DateTime.Now;
            var appealDate = DateTime.Now.AddDays(1);
            var cancellationDate = DateTime.Now.AddDays(2);
            var cancellationReason = "Test cancellation reason";

            // Act
            var project = new Project(title, keyWord1, keyWord2, keyWord3, isScholarshipCandidate,
                                       objective, methodology, expectedResults, activitiesExecutionSchedule,
                                       studentId, programTypeId, professorId, subAreaId, noticeId,
                                       status, statusDescription, appealDescription,
                                       submissionDate, appealDate, cancellationDate,
                                       cancellationReason);

            // Assert
            project.Should().NotBeNull();
            project.Title.Should().Be(title);
            project.KeyWord1.Should().Be(keyWord1);
            project.KeyWord2.Should().Be(keyWord2);
            project.KeyWord3.Should().Be(keyWord3);
            project.IsScholarshipCandidate.Should().Be(isScholarshipCandidate);
            project.Objective.Should().Be(objective);
            project.Methodology.Should().Be(methodology);
            project.ExpectedResults.Should().Be(expectedResults);
            project.ActivitiesExecutionSchedule.Should().Be(activitiesExecutionSchedule);
            project.StudentId.Should().Be(studentId);
            project.ProgramTypeId.Should().Be(programTypeId);
            project.ProfessorId.Should().Be(professorId);
            project.SubAreaId.Should().Be(subAreaId);
            project.NoticeId.Should().Be(noticeId);
            project.Status.Should().Be(status);
            project.StatusDescription.Should().Be(statusDescription);
            project.AppealObservation.Should().Be(appealDescription);
            project.SubmissionDate.Should().Be(submissionDate);
            project.AppealDate.Should().Be(appealDate);
            project.CancellationDate.Should().Be(cancellationDate);
            project.CancellationReason.Should().Be(cancellationReason);
        }
    }
}
