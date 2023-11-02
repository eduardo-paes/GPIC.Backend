using Application.Interfaces.UseCases.ProjectPartialReport;
using Application.Ports.ProjectPartialReport;
using Application.Tests.Mocks;
using Application.Validation;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.ProjectPartialReport
{
    public class GetProjectPartialReportByProjectIdTests
    {
        private readonly Mock<IProjectPartialReportRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IGetProjectPartialReportByProjectId CreateUseCase() => new Application.UseCases.ProjectPartialReport.GetProjectPartialReportByProjectId(_repositoryMock.Object, _mapperMock.Object);

        [Fact]
        public async Task ExecuteAsync_ValidProjectId_ReturnsDetailedReadProjectPartialReportOutput()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var useCase = CreateUseCase();
            var projectPartialReport = ProjectPartialReportMock.MockValidProjectPartialReport();

            _repositoryMock.Setup(repo => repo.GetByProjectIdAsync(projectId)).ReturnsAsync(projectPartialReport);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadProjectPartialReportOutput>(projectPartialReport)).Returns(new DetailedReadProjectPartialReportOutput());

            // Act
            var result = await useCase.ExecuteAsync(projectId);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(repo => repo.GetByProjectIdAsync(projectId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProjectPartialReportOutput>(projectPartialReport), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_NullProjectId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();

            // Act & Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(null));
        }
    }
}
