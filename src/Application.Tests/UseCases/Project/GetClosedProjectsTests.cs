using Application.Interfaces.UseCases.Project;
using Application.Ports.Project;
using Application.Tests.Mocks;
using Application.UseCases.Project;
using Application.Validation;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.Project
{
    public class GetClosedProjectsTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<ITokenAuthenticationService> _tokenAuthenticationServiceMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IGetClosedProjects CreateUseCase() => new GetClosedProjects(
            _projectRepositoryMock.Object,
            _tokenAuthenticationServiceMock.Object,
            _mapperMock.Object
        );

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsResumedReadProjectOutputList()
        {
            // Arrange
            var useCase = CreateUseCase();
            var skip = 0;
            var take = 10;
            var professorId = Guid.NewGuid();
            var userClaims = ClaimsMock.MockValidClaims();
            var projects = new List<Domain.Entities.Project>
            {
                ProjectMock.MockValidProject(),
                ProjectMock.MockValidProject()
            };

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectRepositoryMock.Setup(repo => repo.GetProfessorProjectsAsync(skip, take, It.IsAny<Guid>(), true))
                .ReturnsAsync(projects);
            _mapperMock.Setup(mapper => mapper.Map<IList<ResumedReadProjectOutput>>(projects))
                .Returns(new List<ResumedReadProjectOutput> { new(), new() });

            // Act
            var result = await useCase.ExecuteAsync(skip, take);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count);
            _tokenAuthenticationServiceMock.Verify(service => service.GetUserAuthenticatedClaims(), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.GetProfessorProjectsAsync(skip, take, It.IsAny<Guid>(), true), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IList<ResumedReadProjectOutput>>(projects), Times.Once);
        }



        [Fact]
        public async Task ExecuteAsync_InvalidSkipAndTake_ThrowsArgumentException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var skip = -1;
            var take = 0;

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await useCase.ExecuteAsync(skip, take));

            // Additional assertions
            _tokenAuthenticationServiceMock.Verify(service => service.GetUserAuthenticatedClaims(), Times.Never);
            _projectRepositoryMock.Verify(repo => repo.GetProfessorProjectsAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Guid>(), It.IsAny<bool>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<IList<ResumedReadProjectOutput>>(It.IsAny<List<Domain.Entities.Project>>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_UnauthorizedUser_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var skip = 0;
            var take = 10;

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(null as Dictionary<Guid, Domain.Entities.User>);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(skip, take));

            // Additional assertions
            _tokenAuthenticationServiceMock.Verify(service => service.GetUserAuthenticatedClaims(), Times.Once);
            _projectRepositoryMock.Verify(repo => repo.GetProfessorProjectsAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Guid>(), It.IsAny<bool>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<IList<ResumedReadProjectOutput>>(It.IsAny<IList<Domain.Entities.Project>>), Times.Never);
        }
    }
}
