using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Application.Interfaces.UseCases.Notice;
using Application.Ports.Notice;
using Application.UseCases.Notice;
using Moq;
using Xunit;
using Application.Validation;
using Application.Ports.Activity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Tests.UseCases.Notice
{
    public class CreateNoticeTests
    {
        private readonly Mock<INoticeRepository> _repositoryMock = new();
        private readonly Mock<IStorageFileService> _storageFileServiceMock = new();
        private readonly Mock<IActivityTypeRepository> _activityTypeRepositoryMock = new();
        private readonly Mock<IActivityRepository> _activityRepositoryMock = new();
        private readonly Mock<IProfessorRepository> _professorRepositoryMock = new();
        private readonly Mock<IEmailService> _emailServiceMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<ILogger<CreateNotice>> _loggerMock = new();

        private ICreateNotice CreateUseCase() => new CreateNotice(
            _repositoryMock.Object,
            _storageFileServiceMock.Object,
            _activityTypeRepositoryMock.Object,
            _activityRepositoryMock.Object,
            _professorRepositoryMock.Object,
            _emailServiceMock.Object,
            _mapperMock.Object,
            _loggerMock.Object);

        private CreateNoticeInput MockValidNoticeInput() => new CreateNoticeInput
        {
            RegistrationStartDate = DateTime.UtcNow,
            RegistrationEndDate = DateTime.UtcNow.AddDays(7),
            EvaluationStartDate = DateTime.UtcNow.AddDays(8),
            EvaluationEndDate = DateTime.UtcNow.AddDays(14),
            AppealStartDate = DateTime.UtcNow.AddDays(15),
            AppealEndDate = DateTime.UtcNow.AddDays(21),
            SendingDocsStartDate = DateTime.UtcNow.AddDays(22),
            SendingDocsEndDate = DateTime.UtcNow.AddDays(28),
            PartialReportDeadline = DateTime.UtcNow.AddDays(29),
            FinalReportDeadline = DateTime.UtcNow.AddDays(35),
            Description = "Description",
            SuspensionYears = 1,
            File = new Mock<IFormFile>().Object,
            Activities = new List<CreateActivityTypeInput>
                {
                    new() {
                        Name = "Activity Type 1",
                        Unity = "Unity 1",
                        Activities = new List<CreateActivityInput>
                        {
                            new() {
                                Name = "Activity 1",
                                Points = 10,
                                Limits = 100
                            }
                        }
                    }
                }
        };

        private Domain.Entities.Notice MockValidNotice() => new(
            id: Guid.NewGuid(),
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
            description: "Description",
            suspensionYears: 1
        );

        private static Domain.Entities.ActivityType MockValidActivityType() =>
            new(Guid.NewGuid(), "Activity Type 1", "Unity 1", Guid.NewGuid());

        private static Domain.Entities.Activity MockValidActivity() =>
            new(Guid.NewGuid(), "Activity 1", 10, 100, Guid.NewGuid());

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadNoticeOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var notice = MockValidNotice();
            notice.DocUrl = "FileUrl";
            var activityType = MockValidActivityType();
            var activity = MockValidActivity();
            var input = MockValidNoticeInput();
            var professors = new List<Domain.Entities.Professor>()
            {
                new Domain.Entities.Professor("1234567", 12345)
            };

            _repositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Notice>())).ReturnsAsync(notice);
            _repositoryMock.Setup(repo => repo.GetNoticeByPeriodAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync((Domain.Entities.Notice)null);
            _storageFileServiceMock.Setup(service => service.UploadFileAsync(input.File, null)).ReturnsAsync("FileUrl");
            _activityTypeRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.ActivityType>())).ReturnsAsync(activityType);
            _activityRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Activity>())).ReturnsAsync(activity);
            _professorRepositoryMock.Setup(repo => repo.GetAllActiveProfessorsAsync()).ReturnsAsync(professors);
            _emailServiceMock.Setup(service => service.SendNoticeEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadNoticeOutput>(It.IsAny<Domain.Entities.Notice>())).Returns(new DetailedReadNoticeOutput());

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Notice>()), Times.Once);
            _repositoryMock.Verify(repo => repo.GetNoticeByPeriodAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
            _storageFileServiceMock.Verify(service => service.UploadFileAsync(input.File, null), Times.Once);
            _activityTypeRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.ActivityType>()), Times.Exactly(1));
            _activityRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Activity>()), Times.Exactly(1));
            _professorRepositoryMock.Verify(repo => repo.GetAllActiveProfessorsAsync(), Times.Once);
            _emailServiceMock.Verify(service => service.SendNoticeEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>()), Times.Exactly(1));
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadNoticeOutput>(It.IsAny<Domain.Entities.Notice>()), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_NoActivitiesProvided_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var notice = MockValidNotice();
            var input = MockValidNoticeInput();
            input.Activities = null;

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _repositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Notice>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_NoticeForPeriodAlreadyExists_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var notice = MockValidNotice();
            var input = MockValidNoticeInput();

            _repositoryMock.Setup(repo => repo.GetNoticeByPeriodAsync((DateTime)input.RegistrationStartDate, (DateTime)input.RegistrationEndDate))
                .ReturnsAsync(notice);

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _repositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Notice>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_FileUploadFails_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var notice = MockValidNotice();
            var input = MockValidNoticeInput();

            _repositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Notice>())).ReturnsAsync(notice);
            _repositoryMock.Setup(repo => repo.GetNoticeByPeriodAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync((Domain.Entities.Notice)null);
            _storageFileServiceMock.Setup(service => service.UploadFileAsync(input.File, null))
                .ThrowsAsync(new Exception(It.IsAny<string>()));

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _repositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Notice>()), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_ActivityValidationFails_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var notice = MockValidNotice();
            notice.DocUrl = "FileUrl";
            var activityType = MockValidActivityType();
            var activity = MockValidActivity();
            var input = MockValidNoticeInput();

            input.Activities![0].Name = null;
            input.Activities![0].Activities![0].Name = null;

            _repositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Notice>())).ReturnsAsync(notice);
            _repositoryMock.Setup(repo => repo.GetNoticeByPeriodAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync((Domain.Entities.Notice)null);
            _storageFileServiceMock.Setup(service => service.UploadFileAsync(input.File, null)).ReturnsAsync("FileUrl");

            // Act & Assert
            Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(input));
            _repositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Notice>()), Times.Never);
        }
    }
}
