using Application.Interfaces.UseCases.Notice;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Moq;
using Xunit;
using Application.UseCases.Notice;
using Application.Tests.Mocks;

namespace Application.Tests.UseCases.Notice
{
    public class ReportDeadlineNotificationTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IEmailService> _emailServiceMock = new();

        private IReportDeadlineNotification CreateUseCase() => new ReportDeadlineNotification(_projectRepositoryMock.Object, _emailServiceMock.Object);

        [Fact]
        public async Task ExecuteAsync_ProjectsWithCloseReportDueDate_ReturnsSuccessMessage()
        {
            // Arrange
            var useCase = CreateUseCase();
            var nextWeek = DateTime.UtcNow.AddDays(7).Date;
            var nextMonth = DateTime.UtcNow.AddMonths(1).Date;

            var projects = new List<Domain.Entities.Project>
            {
                ProjectMock.MockValidProjectProfessorAndNotice(),
                ProjectMock.MockValidProjectProfessorAndNotice()
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
            var projects = new List<Domain.Entities.Project>();

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
