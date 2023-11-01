using Xunit;
using Moq;
using Application.UseCases.ProjectFinalReport;
using Application.Interfaces.UseCases.ProjectFinalReport;
using Application.Ports.ProjectFinalReport;
using System;
using System.Threading.Tasks;
using Application.Validation;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Entities.Enums;
using Application.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Application.Tests.UseCases.ProjectFinalReport
{
    public class UpdateProjectFinalReportTests
    {
        private readonly Mock<IProjectFinalReportRepository> _projectReportRepositoryMock = new();
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IStorageFileService> _storageFileServiceMock = new();
        private readonly Mock<ITokenAuthenticationService> _tokenAuthenticationServiceMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IUpdateProjectFinalReport CreateUseCase() => new UpdateProjectFinalReport(
            _projectReportRepositoryMock.Object,
            _projectRepositoryMock.Object,
            _storageFileServiceMock.Object,
            _tokenAuthenticationServiceMock.Object,
            _mapperMock.Object
        );

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadProjectFinalReportOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var input = new UpdateProjectFinalReportInput
            {
                ReportFile = FileMock.CreateIFormFile()
            };
            var report = ProjectFinalReportMock.MockValidProjectFinalReportWithId();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            project.Status = EProjectStatus.Started;

            var userClaims = ClaimsMock.MockValidClaims();
            project.ProfessorId = userClaims.Keys.FirstOrDefault();

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectReportRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(report);
            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(project);
            _storageFileServiceMock.Setup(service => service.UploadFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>()));
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadProjectFinalReportOutput>(It.IsAny<Domain.Entities.ProjectFinalReport>()))
                .Returns(new DetailedReadProjectFinalReportOutput());

            // Act
            var result = await useCase.ExecuteAsync(id, input);

            // Assert
            Assert.NotNull(result);
            _tokenAuthenticationServiceMock.Verify(service => service.GetUserAuthenticatedClaims(), Times.Once);
            _projectReportRepositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _storageFileServiceMock.Verify(service => service.UploadFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>()), Times.Once);
            _projectReportRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.ProjectFinalReport>()), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProjectFinalReportOutput>(It.IsAny<Domain.Entities.ProjectFinalReport>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_NoId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid? id = null;
            var input = new UpdateProjectFinalReportInput
            {
                ReportFile = FileMock.CreateIFormFile()
            };

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
        }

        [Fact]
        public async Task ExecuteAsync_NoReportFile_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var input = new UpdateProjectFinalReportInput
            {
                ReportFile = null // No report file provided
            };

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
        }
    }
}
