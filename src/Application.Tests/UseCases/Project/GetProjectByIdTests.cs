using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.UseCases.Project;
using Application.Ports.Project;
using Application.Validation;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Application.Interfaces.UseCases.Project;
using Application.Tests.Mocks;

namespace Application.Tests.UseCases.Project
{
    public class GetProjectByIdTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IGetProjectById CreateUseCase() => new GetProjectById(_projectRepositoryMock.Object, _mapperMock.Object);

        [Fact]
        public async Task ExecuteAsync_ValidId_ReturnsDetailedReadProjectOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var project = ProjectMock.MockValidProjectProfessorAndNotice();
            var projectId = project.Id;

            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(projectId)).ReturnsAsync(project);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadProjectOutput>(project)).Returns(new DetailedReadProjectOutput());

            // Act
            var result = await useCase.ExecuteAsync(projectId);

            // Assert
            Assert.NotNull(result);
            _projectRepositoryMock.Verify(repo => repo.GetByIdAsync(projectId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProjectOutput>(project), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_InvalidId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var projectId = Guid.NewGuid();

            _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(projectId)).ReturnsAsync((Domain.Entities.Project)null);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(projectId));

            _projectRepositoryMock.Verify(repo => repo.GetByIdAsync(projectId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadProjectOutput>(It.IsAny<Domain.Entities.Project>()), Times.Never);
        }
    }
}
