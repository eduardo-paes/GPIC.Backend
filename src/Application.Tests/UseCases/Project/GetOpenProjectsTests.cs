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
    public class GetOpenProjectsTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<ITokenAuthenticationService> _tokenAuthenticationServiceMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IGetOpenProjects CreateUseCase() => new GetOpenProjects(
            _projectRepositoryMock.Object,
            _tokenAuthenticationServiceMock.Object,
            _mapperMock.Object
        );

        [Fact]
        public async Task ExecuteAsync_WithValidInput_ReturnsProjectList()
        {
            // Arrange
            var userClaims = ClaimsMock.MockValidClaims();
            var useCase = CreateUseCase();

            var skip = 0;
            var take = 10;
            var onlyMyProjects = true;

            _projectRepositoryMock.Setup(repo => repo.GetProfessorProjectsAsync(skip, take, It.IsAny<Guid>(), false))
                .ReturnsAsync(new List<Domain.Entities.Project>());

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims())
                .Returns(userClaims);

            _mapperMock.Setup(mapper => mapper.Map<IList<ResumedReadProjectOutput>>(It.IsAny<IEnumerable<Domain.Entities.Project>>()))
                .Returns(new List<ResumedReadProjectOutput>());

            // Act
            var result = await useCase.ExecuteAsync(skip, take, onlyMyProjects);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ResumedReadProjectOutput>>(result);
            _projectRepositoryMock.Verify(repo => repo.GetProfessorProjectsAsync(skip, take, It.IsAny<Guid>(), false), Times.Once);
            _tokenAuthenticationServiceMock.Verify(service => service.GetUserAuthenticatedClaims(), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IList<ResumedReadProjectOutput>>(It.IsAny<IEnumerable<Domain.Entities.Project>>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_InvalidSkipOrTake_ThrowsArgumentException()
        {
            // Arrange
            var useCase = CreateUseCase();

            var skip = -1; // Invalid skip value
            var take = 0;  // Invalid take value
            var onlyMyProjects = true;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await useCase.ExecuteAsync(skip, take, onlyMyProjects));
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
