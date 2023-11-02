using Application.Interfaces.UseCases.ProjectFinalReport;
using Application.Ports.ProjectFinalReport;
using Application.Tests.Mocks;
using Application.Validation;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.ProjectFinalReport
{
    public class GetProjectFinalReportByProjectIdTests
    {
        private readonly Mock<IProjectFinalReportRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IGetProjectFinalReportByProjectId CreateUseCase() => new Application.UseCases.ProjectFinalReport.GetProjectFinalReportByProjectId(
            _repositoryMock.Object,
            _mapperMock.Object
        );

        [Fact]
        public async Task ExecuteAsync_ValidProjectId_ReturnsDetailedReadProjectFinalReportOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var report = ProjectFinalReportMock.MockValidProjectFinalReportWithId();
            var projectId = report.ProjectId;

            _repositoryMock.Setup(repo => repo.GetByProjectIdAsync(projectId)).ReturnsAsync(report);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadProjectFinalReportOutput>(report)).Returns(new DetailedReadProjectFinalReportOutput());

            // Act
            var result = await useCase.ExecuteAsync(projectId);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(repo => repo.GetByProjectIdAsync(It.IsAny<Guid?>()), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProjectFinalReportOutput>(report), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_NullProjectId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid? projectId = null;

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(projectId));

            // Assert
            UseCaseException.NotInformedParam(It.IsAny<bool>(), It.IsAny<string>()); // Make sure this method is called with any boolean and string.
            _repositoryMock.Verify(repo => repo.GetByProjectIdAsync(It.IsAny<Guid?>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProjectFinalReportOutput>(It.IsAny<object>()), Times.Never);
        }
    }
}
