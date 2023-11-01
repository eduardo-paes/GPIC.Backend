using Application.Interfaces.UseCases.Project;
using Application.Tests.Mocks;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.Project
{
    public class ClosePendingProjectsTests
    {

        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private IClosePendingProjects CreateUseCase() => new Application.UseCases.Project.ClosePendingProjects(
            _projectRepositoryMock.Object
        );

        [Fact]
        public async Task ExecuteAsync_NoPendingProjects_ReturnsSuccessMessage()
        {
            // Arrange
            var useCase = CreateUseCase();
            var projects = new List<Domain.Entities.Project>();

            _projectRepositoryMock.Setup(repo => repo.GetPendingAndOverdueProjectsAsync()).ReturnsAsync(projects);

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            Assert.Equal("Nenhum projeto pendente e com prazo vencido foi encontrado.", result);
        }

        [Fact]
        public async Task ExecuteAsync_CancelProjects_SuccessfullyCancelsProjects()
        {
            // Arrange
            var useCase = CreateUseCase();
            var project1 = ProjectMock.MockValidProjectWithId();
            var project2 = ProjectMock.MockValidProjectWithId();
            var projects = new List<Domain.Entities.Project> { project1, project2 };

            _projectRepositoryMock.Setup(repo => repo.GetPendingAndOverdueProjectsAsync()).ReturnsAsync(projects);
            _projectRepositoryMock.Setup(repo => repo.UpdateManyAsync(It.IsAny<IList<Domain.Entities.Project>>())).ReturnsAsync(projects.Count);

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            _projectRepositoryMock.Verify(repo => repo.UpdateManyAsync(It.IsAny<IList<Domain.Entities.Project>>()), Times.Once);
            Assert.Equal($"{projects.Count} projetos pendentes e com prazo de resolução vencido foram cancelados com sucesso.", result);
            Assert.All(projects, project =>
            {
                Assert.Equal(EProjectStatus.Canceled, project.Status);
                Assert.Equal("Projeto cancelado automaticamente por falta de ação dentro do prazo estipulado.", project.StatusDescription);
            });
        }

        [Fact]
        public async Task ExecuteAsync_CancelProjects_PartiallyCancelsProjects()
        {
            // Arrange
            var useCase = CreateUseCase();
            var project1 = ProjectMock.MockValidProjectWithId();
            project1.Status = EProjectStatus.Pending;
            var project2 = ProjectMock.MockValidProjectWithId();
            project2.Status = EProjectStatus.Rejected;
            var projects = new List<Domain.Entities.Project> { project1, project2 };

            _projectRepositoryMock.Setup(repo => repo.GetPendingAndOverdueProjectsAsync()).ReturnsAsync(projects);
            _projectRepositoryMock.Setup(repo => repo.UpdateManyAsync(It.IsAny<IList<Domain.Entities.Project>>())).ReturnsAsync(1); // Simulate a partial update

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            _projectRepositoryMock.Verify(repo => repo.UpdateManyAsync(It.IsAny<IList<Domain.Entities.Project>>()), Times.Once);
            Assert.Equal($"Ocorreu um erro ao cancelar os projetos pendentes e com prazo vencido. Foram cancelados 1 de {projects.Count} projetos.", result);
        }
    }
}
