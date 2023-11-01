using AutoMapper;
using Domain.Interfaces.Repositories;
using Moq;
using Xunit;
using Application.Interfaces.UseCases.ProjectPartialReport;
using Application.UseCases.ProjectPartialReport;
using Application.Ports.ProjectPartialReport;
using Application.Validation;
using Application.Tests.Mocks;

namespace Application.Tests.UseCases.ProjectPartialReport
{
    public class DeleteProjectPartialReportTests
    {
        private readonly Mock<IProjectPartialReportRepository> _repositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IDeleteProjectPartialReport CreateUseCase() =>
            new DeleteProjectPartialReport(_repositoryMock.Object, _mapperMock.Object);

        [Fact]
        public async Task ExecuteAsync_ValidId_ReturnsDetailedReadProjectPartialReportOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var projectPartialReport = ProjectPartialReportMock.MockValidProjectPartialReport();

            _repositoryMock.Setup(repo => repo.DeleteAsync(id)).ReturnsAsync(projectPartialReport);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadProjectPartialReportOutput>(projectPartialReport))
                .Returns(new DetailedReadProjectPartialReportOutput());

            // Act
            var result = await useCase.ExecuteAsync(id);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(repo => repo.DeleteAsync(id), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProjectPartialReportOutput>(projectPartialReport), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_NullId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();

            // Act & Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(null));
        }
    }
}