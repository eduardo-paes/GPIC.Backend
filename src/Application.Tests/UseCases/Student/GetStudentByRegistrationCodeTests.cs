using Application.Interfaces.UseCases.Student;
using Application.Ports.Student;
using Application.Tests.Mocks;
using Application.UseCases.Student;
using Application.Validation;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.Student
{
    public class GetStudentByRegistrationCodeTests
    {
        private readonly Mock<IStudentRepository> _studentRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IGetStudentByRegistrationCode CreateUseCase() =>
            new GetStudentByRegistrationCode(_studentRepositoryMock.Object, _mapperMock.Object);

        [Fact]
        public async Task ExecuteAsync_WithValidRegistrationCode_ReturnsDetailedReadStudentOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var registrationCode = "ABC123";
            var student = StudentMock.MockValidStudent();
            var expectedOutput = new DetailedReadStudentOutput();

            _studentRepositoryMock.Setup(repo => repo.GetByRegistrationCodeAsync(registrationCode)).ReturnsAsync(student);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadStudentOutput>(student)).Returns(expectedOutput);

            // Act
            var result = await useCase.ExecuteAsync(registrationCode);

            // Assert
            Assert.NotNull(result);
            _studentRepositoryMock.Verify(repo => repo.GetByRegistrationCodeAsync(registrationCode), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadStudentOutput>(student), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_WithEmptyRegistrationCode_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            string registrationCode = string.Empty;

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(registrationCode));
            _studentRepositoryMock.Verify(repo => repo.GetByRegistrationCodeAsync(It.IsAny<string>()), Times.Never);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadStudentOutput>(It.IsAny<Domain.Entities.Student>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_WithInvalidRegistrationCode_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            string registrationCode = "InvalidCode";
            _studentRepositoryMock.Setup(repo => repo.GetByRegistrationCodeAsync(registrationCode)).ReturnsAsync((Domain.Entities.Student)null);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(registrationCode));
            _studentRepositoryMock.Verify(repo => repo.GetByRegistrationCodeAsync(registrationCode), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadStudentOutput>(It.IsAny<Domain.Entities.Student>()), Times.Never);
        }
    }
}
