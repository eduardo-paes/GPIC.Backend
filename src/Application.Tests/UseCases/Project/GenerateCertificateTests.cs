using Application.Interfaces.UseCases.Project;
using Application.Tests.Mocks;
using Application.UseCases.Project;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.Project
{
    public class GenerateCertificateTests
    {
        private readonly Mock<INoticeRepository> _noticeRepositoryMock = new();
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IProfessorRepository> _professorRepositoryMock = new();
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<IProjectFinalReportRepository> _projectReportRepositoryMock = new();
        private readonly Mock<IReportService> _reportServiceMock = new();
        private readonly Mock<IStorageFileService> _storageFileServiceMock = new();

        private IGenerateCertificate CreateUseCase() => new GenerateCertificate(
            projectRepository: _projectRepositoryMock.Object,
            noticeRepository: _noticeRepositoryMock.Object,
            projectReportRepository: _projectReportRepositoryMock.Object,
            professorRepository: _professorRepositoryMock.Object,
            userRepository: _userRepositoryMock.Object,
            reportService: _reportServiceMock.Object,
            storageFileService: _storageFileServiceMock.Object
        );

        [Fact]
        public async Task ExecuteAsync_NoticeNotFound_ReturnsErrorMessage()
        {
            // Arrange
            var useCase = CreateUseCase();
            _noticeRepositoryMock.Setup(repo => repo.GetNoticeEndingAsync()).ReturnsAsync((Domain.Entities.Notice)null);

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            Assert.Equal("Nenhum edital em estágio de encerramento encontrado.", result);
        }

        [Fact]
        public async Task ExecuteAsync_NoProjectsFound_ReturnsErrorMessage()
        {
            // Arrange
            var useCase = CreateUseCase();

            _noticeRepositoryMock.Setup(repo => repo.GetNoticeEndingAsync()).ReturnsAsync(NoticeMock.MockValidNotice());
            _projectRepositoryMock.Setup(repo => repo.GetProjectByNoticeAsync(It.IsAny<Guid>())).ReturnsAsync(Enumerable.Empty<Domain.Entities.Project>());

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            Assert.Equal("Nenhum projeto em estágio de encerramento encontrado.", result);
        }

        [Fact]
        public async Task ExecuteAsync_CoordinatorNotFound_ReturnsErrorMessage()
        {
            // Arrange
            var useCase = CreateUseCase();
            var projects = new[] { ProjectMock.MockValidProjectProfessorAndNotice() };
            var notice = NoticeMock.MockValidNoticeWithId();

            _noticeRepositoryMock.Setup(repo => repo.GetNoticeEndingAsync()).ReturnsAsync(notice);
            _projectRepositoryMock.Setup(repo => repo.GetProjectByNoticeAsync(It.IsAny<Guid>())).ReturnsAsync(projects);
            _userRepositoryMock.Setup(repo => repo.GetCoordinatorAsync()).ReturnsAsync((Domain.Entities.User)null);

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            Assert.Equal("Nenhum coordenador encontrado.", result);
        }

        [Fact]
        public async Task ExecuteAsync_ProjectWithoutFinalReport_ClosesProjectAndSuspendsProfessor()
        {
            // Arrange
            var useCase = CreateUseCase();
            var projects = new[] { ProjectMock.MockValidProjectProfessorAndNotice() };
            var notice = NoticeMock.MockValidNoticeWithId();
            var coordinator = UserMock.MockValidUser();

            _noticeRepositoryMock.Setup(repo => repo.GetNoticeEndingAsync()).ReturnsAsync(notice);
            _projectRepositoryMock.Setup(repo => repo.GetProjectByNoticeAsync(It.IsAny<Guid>())).ReturnsAsync(projects);
            _userRepositoryMock.Setup(repo => repo.GetCoordinatorAsync()).ReturnsAsync(coordinator);
            _projectRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Project>())).ReturnsAsync((Domain.Entities.Project project) => project);
            _projectReportRepositoryMock.Setup(repo => repo.GetByProjectIdAsync(It.IsAny<Guid?>())).ReturnsAsync((Domain.Entities.ProjectFinalReport)null);
            _professorRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid?>())).ReturnsAsync((Domain.Entities.Professor)null);

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            Assert.Equal("Certificados gerados com sucesso.", result);

            // Verify project status and professor suspension
            _noticeRepositoryMock.Verify(repo => repo.GetNoticeEndingAsync(), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Project>()), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetCoordinatorAsync(), Times.Once);
            _projectReportRepositoryMock.Verify(repo => repo.GetByProjectIdAsync(It.IsAny<Guid?>()), Times.Once);
            _professorRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid?>()), Times.Once);
            _professorRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Professor>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_ProjectWithFinalReport_GeneratesCertificateAndClosesProject()
        {
            // Arrange
            var useCase = CreateUseCase();
            var coordinator = UserMock.MockValidUser();
            var notice = NoticeMock.MockValidNoticeWithId();
            var projects = new[] { ProjectMock.MockValidProjectProfessorAndNotice() };
            var projectFinalReport = new Domain.Entities.ProjectFinalReport(Guid.NewGuid(), Guid.NewGuid());
            var file = FileMock.CreateIFormFile();
            var path = "./Samples/sample.pdf";
            var bytes = File.ReadAllBytes(path);

            _noticeRepositoryMock.Setup(repo => repo.GetNoticeEndingAsync()).ReturnsAsync(notice);
            _projectRepositoryMock.Setup(repo => repo.GetProjectByNoticeAsync(It.IsAny<Guid>())).ReturnsAsync(projects);
            _userRepositoryMock.Setup(repo => repo.GetCoordinatorAsync()).ReturnsAsync(coordinator);
            _projectRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Project>())).ReturnsAsync((Domain.Entities.Project project) => project);
            _projectReportRepositoryMock.Setup(repo => repo.GetByProjectIdAsync(It.IsAny<Guid>())).ReturnsAsync(projectFinalReport);
            _reportServiceMock.Setup(service => service.GenerateCertificateAsync(It.IsAny<Domain.Entities.Project>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(path);
            _storageFileServiceMock.Setup(service => service.UploadFileAsync(bytes, path)).ReturnsAsync("certificate_url");

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            Assert.Equal("Certificados gerados com sucesso.", result);

            // Verify project status and certificate generation
            _noticeRepositoryMock.Verify(repo => repo.GetNoticeEndingAsync(), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.GetProjectByNoticeAsync(It.IsAny<Guid>()), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetCoordinatorAsync(), Times.Once);
            _projectReportRepositoryMock.Verify(repo => repo.GetByProjectIdAsync(It.IsAny<Guid?>()), Times.Once);
            _reportServiceMock.Verify(service => service.GenerateCertificateAsync(It.IsAny<Domain.Entities.Project>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _storageFileServiceMock.Verify(service => service.UploadFileAsync(bytes, path), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Project>()), Times.Once);
        }
    }
}
