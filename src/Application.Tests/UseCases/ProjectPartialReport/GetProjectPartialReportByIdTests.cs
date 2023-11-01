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
    public class GetProjectPartialReportByIdTests
    {
        private readonly Mock<IProjectPartialReportRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IGetProjectPartialReportById CreateUseCase() => new Application.UseCases.ProjectPartialReport.GetProjectPartialReportById(_repositoryMock.Object, _mapperMock.Object);

        [Fact]
        public async Task ExecuteAsync_ValidId_ReturnsDetailedReadProjectPartialReportOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var projectPartialReport = ProjectPartialReportMock.MockValidProjectPartialReport();
            var detailedReadProjectPartialReportOutput = new DetailedReadProjectPartialReportOutput();

            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(projectPartialReport);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadProjectPartialReportOutput>(projectPartialReport)).Returns(detailedReadProjectPartialReportOutput);

            // Act
            var result = await useCase.ExecuteAsync(id);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProjectPartialReportOutput>(projectPartialReport), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_NullId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid? id = null;

            // Act & Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id));
        }

        [Fact]
        public async Task ExecuteAsync_IdNotFound_ReturnsDetailedReadProjectPartialReportOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();

            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Domain.Entities.ProjectPartialReport)null);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadProjectPartialReportOutput>(It.IsAny<Domain.Entities.ProjectPartialReport>())).Returns(new DetailedReadProjectPartialReportOutput());

            // Act
            var result = await useCase.ExecuteAsync(id);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProjectPartialReportOutput>(It.IsAny<Domain.Entities.ProjectPartialReport>()), Times.Once);
        }
    }
}
