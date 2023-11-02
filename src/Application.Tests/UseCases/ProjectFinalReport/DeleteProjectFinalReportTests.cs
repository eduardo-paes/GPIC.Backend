using Application.Interfaces.UseCases.ProjectFinalReport;
using Application.Ports.ProjectFinalReport;
using Application.Tests.Mocks;
using Application.UseCases.ProjectFinalReport;
using Application.Validation;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.ProjectFinalReport
{
    public class DeleteProjectFinalReportTests
    {
        private readonly Mock<IProjectFinalReportRepository> _projectReportRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IDeleteProjectFinalReport CreateUseCase() => new DeleteProjectFinalReport(
            _projectReportRepositoryMock.Object,
            _mapperMock.Object
        );

        [Fact]
        public async Task ExecuteAsync_ValidId_ReturnsDetailedReadProjectFinalReportOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var projectId = Guid.NewGuid();
            var projectFinalReport = ProjectFinalReportMock.MockValidProjectFinalReport();
            projectFinalReport.ProjectId = projectId;
            var detailedReadOutput = new DetailedReadProjectFinalReportOutput();

            _projectReportRepositoryMock.Setup(repo => repo.DeleteAsync(projectId)).ReturnsAsync(projectFinalReport);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadProjectFinalReportOutput>(projectFinalReport)).Returns(detailedReadOutput);

            // Act
            var result = await useCase.ExecuteAsync(projectId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(detailedReadOutput, result);
            _projectReportRepositoryMock.Verify(repo => repo.DeleteAsync(projectId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProjectFinalReportOutput>(projectFinalReport), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_NullId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();

            // Act & Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(null));
            _projectReportRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Guid>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProjectFinalReportOutput>(It.IsAny<Domain.Entities.ProjectFinalReport>()), Times.Never);
        }
    }
}
