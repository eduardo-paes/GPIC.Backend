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
    public class GetProjectsToEvaluateTests
    {
        private readonly Mock<ITokenAuthenticationService> _tokenAuthenticationServiceMock = new();
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IGetProjectsToEvaluate CreateUseCase() => new GetProjectsToEvaluate(
            _tokenAuthenticationServiceMock.Object,
            _projectRepositoryMock.Object,
            _mapperMock.Object
        );

        [Fact]
        public async Task ExecuteAsync_InvalidParameters_ThrowsArgumentException()
        {
            // Arrange
            var useCase = CreateUseCase();

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await useCase.ExecuteAsync(-1, 0));
        }

        [Fact]
        public async Task ExecuteAsync_UserNotAdminOrProfessor_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var userClaims = ClaimsMock.MockValidClaims();
            userClaims.First().Value.Role = Domain.Entities.Enums.ERole.STUDENT;
            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(0, 1));
        }

        [Fact]
        public async Task ExecuteAsync_ValidUser_GetProjectsToEvaluate()
        {
            // Arrange
            var useCase = CreateUseCase();
            var userClaims = ClaimsMock.MockValidClaims();

            _tokenAuthenticationServiceMock.Setup(service => service.GetUserAuthenticatedClaims()).Returns(userClaims);
            _projectRepositoryMock.Setup(repo => repo.GetProjectsToEvaluateAsync(0, 1, It.IsAny<Guid>()))
                .ReturnsAsync(new List<Domain.Entities.Project>());
            _mapperMock.Setup(mapper => mapper.Map<IList<DetailedReadProjectOutput>>(It.IsAny<List<Domain.Entities.Project>>()))
                .Returns(new List<DetailedReadProjectOutput>());

            // Act
            var result = await useCase.ExecuteAsync(0, 1);

            // Assert
            _projectRepositoryMock.Verify(repo => repo.GetProjectsToEvaluateAsync(0, 1, It.IsAny<Guid>()), Times.Once);
            Assert.NotNull(result);
            _mapperMock.Verify(mapper => mapper.Map<IList<DetailedReadProjectOutput>>(It.IsAny<List<Domain.Entities.Project>>()), Times.Once);
        }
    }
}
