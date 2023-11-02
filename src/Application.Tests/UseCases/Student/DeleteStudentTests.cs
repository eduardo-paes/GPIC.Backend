using Application.Interfaces.UseCases.Student;
using Application.Ports.Student;
using Application.Tests.Mocks;
using Application.Validation;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace Application.Tests.UseCases.Student
{
    public class DeleteStudentTests
    {
        private readonly Mock<IStudentRepository> _studentRepositoryMock = new();
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IDeleteStudent CreateUseCase() => new Application.UseCases.Student.DeleteStudent(
            _studentRepositoryMock.Object,
            _userRepositoryMock.Object,
            _mapperMock.Object
        );

        [Fact]
        public async Task ExecuteAsync_ValidId_DeletesStudentAndUser()
        {
            // Arrange
            var useCase = CreateUseCase();
            var student = StudentMock.MockValidStudentWithId();
            var user = UserMock.MockValidUser();
            var studentId = student.Id;
            var userId = student.UserId;

            _studentRepositoryMock.Setup(repo => repo.GetByIdAsync(studentId)).ReturnsAsync(student);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);
            _studentRepositoryMock.Setup(repo => repo.DeleteAsync(studentId)).ReturnsAsync(student);
            _userRepositoryMock.Setup(repo => repo.DeleteAsync(userId)).ReturnsAsync(user);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadStudentOutput>(student)).Returns(new DetailedReadStudentOutput());

            // Act
            var result = await useCase.ExecuteAsync(studentId);

            // Assert
            Assert.NotNull(result);
            _studentRepositoryMock.Verify(repo => repo.GetByIdAsync(studentId), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(userId), Times.Once);
            _studentRepositoryMock.Verify(repo => repo.DeleteAsync(studentId), Times.Once);
            _userRepositoryMock.Verify(repo => repo.DeleteAsync(userId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadStudentOutput>(student), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_NullId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(null));
        }

        [Fact]
        public async Task ExecuteAsync_InvalidStudentId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var studentId = Guid.NewGuid();

            _studentRepositoryMock.Setup(repo => repo.GetByIdAsync(studentId)).ReturnsAsync((Domain.Entities.Student)null);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(studentId));
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_InvalidUserId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var student = StudentMock.MockValidStudentWithId();
            var user = UserMock.MockValidUser();
            var studentId = student.Id;
            var userId = student.UserId;

            _studentRepositoryMock.Setup(repo => repo.GetByIdAsync(studentId)).ReturnsAsync(student);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync((Domain.Entities.User)null);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(studentId));
            _studentRepositoryMock.Verify(repo => repo.DeleteAsync(studentId), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_StudentNotDeleted_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var student = StudentMock.MockValidStudentWithId();
            var user = UserMock.MockValidUser();
            var studentId = student.Id;
            var userId = student.UserId;

            _studentRepositoryMock.Setup(repo => repo.GetByIdAsync(studentId)).ReturnsAsync(student);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(UserMock.MockValidUser());
            _studentRepositoryMock.Setup(repo => repo.DeleteAsync(studentId)).ReturnsAsync((Domain.Entities.Student)null);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(studentId));
            _userRepositoryMock.Verify(repo => repo.DeleteAsync(userId), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_UserNotDeleted_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var student = StudentMock.MockValidStudentWithId();
            var user = UserMock.MockValidUser();
            var studentId = student.Id;
            var userId = student.UserId;

            _studentRepositoryMock.Setup(repo => repo.GetByIdAsync(studentId)).ReturnsAsync(student);
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(UserMock.MockValidUser());
            _studentRepositoryMock.Setup(repo => repo.DeleteAsync(studentId)).ReturnsAsync(student);
            _userRepositoryMock.Setup(repo => repo.DeleteAsync(userId)).ReturnsAsync((Domain.Entities.User)null);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(studentId));
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadStudentOutput>(student), Times.Never);
        }
    }
}
