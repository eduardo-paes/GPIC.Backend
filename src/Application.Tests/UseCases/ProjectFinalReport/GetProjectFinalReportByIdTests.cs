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
    public class GetProjectFinalReportByIdTests
    {
        private readonly Mock<IProjectFinalReportRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IGetProjectFinalReportById CreateUseCase() =>
            new Application.UseCases.ProjectFinalReport.GetProjectFinalReportById(_repositoryMock.Object, _mapperMock.Object);

        [Fact]
        public async Task ExecuteAsync_ValidId_ReturnsDetailedReadProjectFinalReportOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var projectFinalReport = ProjectFinalReportMock.MockValidProjectFinalReportWithId();
            var id = projectFinalReport.Id;
            var detailedReadProjectFinalReportOutput = new DetailedReadProjectFinalReportOutput();

            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(projectFinalReport);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadProjectFinalReportOutput>(projectFinalReport))
                .Returns(detailedReadProjectFinalReportOutput);

            // Act
            var result = await useCase.ExecuteAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Same(detailedReadProjectFinalReportOutput, result);
            _repositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProjectFinalReportOutput>(projectFinalReport), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_NullId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid? id = null;

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id));
        }

        [Fact]
        public async Task ExecuteAsync_NonExistentId_ReturnsNull()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();

            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Domain.Entities.ProjectFinalReport)null);

            // Act
            var result = await useCase.ExecuteAsync(id);

            // Assert
            Assert.Null(result);
        }
    }
}
