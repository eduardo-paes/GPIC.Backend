using Application.Interfaces.UseCases.Notice;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Moq;
using Xunit;
using Application.UseCases.Notice;
using Domain.Entities;

namespace Application.Tests.UseCases.Notice
{
    public class ReportDeadlineNotificationTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IEmailService> _emailServiceMock = new();

        private IReportDeadlineNotification CreateUseCase() => new ReportDeadlineNotification(_projectRepositoryMock.Object, _emailServiceMock.Object);
        private static Project MockValidProject()
        {
            return new(
                "Project Title",
                "Keyword 1",
                "Keyword 2",
                "Keyword 3",
                true,
                "Objective",
                "Methodology",
                "Expected Results",
                "Schedule",
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Domain.Entities.Enums.EProjectStatus.Opened,
                "Status Description",
                "Appeal Observation",
                DateTime.UtcNow,
                DateTime.UtcNow,
                DateTime.UtcNow,
                "Cancellation Reason")
            {
                Professor = new Domain.Entities.Professor("1234567", 1234567)
                {
                    User = new Domain.Entities.User("Name", "professor@email.com", "Password", "58411338029", Domain.Entities.Enums.ERole.ADMIN)
                },
                Notice = new(
                    registrationStartDate: DateTime.UtcNow,
                    registrationEndDate: DateTime.UtcNow.AddDays(7),
                    evaluationStartDate: DateTime.UtcNow.AddDays(8),
                    evaluationEndDate: DateTime.UtcNow.AddDays(14),
                    appealStartDate: DateTime.UtcNow.AddDays(15),
                    appealFinalDate: DateTime.UtcNow.AddDays(21),
                    sendingDocsStartDate: DateTime.UtcNow.AddDays(22),
                    sendingDocsEndDate: DateTime.UtcNow.AddDays(28),
                    partialReportDeadline: DateTime.UtcNow.AddDays(29),
                    finalReportDeadline: DateTime.UtcNow.AddDays(35),
                    description: "Edital de teste",
                    suspensionYears: 1
                )
            };
        }

        [Fact]
        public async Task ExecuteAsync_ProjectsWithCloseReportDueDate_ReturnsSuccessMessage()
        {
            // Arrange
            var useCase = CreateUseCase();
            var nextWeek = DateTime.UtcNow.AddDays(7).Date;
            var nextMonth = DateTime.UtcNow.AddMonths(1).Date;

            var projects = new List<Project>
            {
                MockValidProject(),
                MockValidProject()
            };

            _projectRepositoryMock.Setup(repo => repo.GetProjectsWithCloseReportDueDateAsync()).ReturnsAsync(projects);
            _emailServiceMock
                .Setup(service => service.SendNotificationOfReportDeadlineEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<DateTime>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            Assert.Equal("Notificação de prazo de entrega de relatório enviada com sucesso.", result);
            _projectRepositoryMock.Verify(repo => repo.GetProjectsWithCloseReportDueDateAsync(), Times.Once);
            _emailServiceMock.Verify(
                service => service.SendNotificationOfReportDeadlineEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<DateTime>()),
                Times.Exactly(2));
        }

        [Fact]
        public async Task ExecuteAsync_NoProjects_ReturnsNoProjectsMessage()
        {
            // Arrange
            var useCase = CreateUseCase();
            var projects = new List<Project>();

            _projectRepositoryMock.Setup(repo => repo.GetProjectsWithCloseReportDueDateAsync()).ReturnsAsync(projects);

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            Assert.Equal("Nenhum projeto com prazo de entrega de relatório próxima.", result);
            _projectRepositoryMock.Verify(repo => repo.GetProjectsWithCloseReportDueDateAsync(), Times.Once);
            _emailServiceMock.Verify(
                service => service.SendNotificationOfReportDeadlineEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<DateTime>()),
                Times.Never);
        }
    }
}
