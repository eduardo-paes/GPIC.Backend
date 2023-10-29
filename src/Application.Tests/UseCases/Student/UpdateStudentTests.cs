using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Student;
using Application.Ports.Student;
using Application.UseCases.Student;
using Application.Validation;
using Moq;
using Xunit;
using Application.Tests.Mocks;

namespace Application.Tests.UseCases.Student
{
    public class UpdateStudentTests
    {
        private readonly Mock<IStudentRepository> _studentRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private IUpdateStudent CreateUseCase() => new UpdateStudent(_studentRepositoryMock.Object, _mapperMock.Object);
        public static UpdateStudentInput GetValidInput()
        {
            return new UpdateStudentInput
            {
                // Required Properties
                RegistrationCode = "ABC123",
                BirthDate = new DateTime(1990, 1, 1),
                RG = 1234567,
                IssuingAgency = "Agency",
                DispatchDate = new DateTime(2000, 1, 1),
                Gender = 0, // Assuming 1 represents a gender value
                Race = 0, // Assuming 2 represents a race value
                HomeAddress = "123 Main St",
                City = "City",
                UF = "CA",
                CEP = 123456,
                CampusId = Guid.NewGuid(),
                CourseId = Guid.NewGuid(),
                StartYear = "2023",
                AssistanceTypeId = Guid.NewGuid(),

                // Optional Properties
                PhoneDDD = 123,
                Phone = 987654321,
                CellPhoneDDD = 456,
                CellPhone = 654321987
            };
        }

        [Fact]
        public async Task ExecuteAsync_ValidInput_ReturnsDetailedReadStudentOutput()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var input = GetValidInput();
            var student = StudentMock.MockValidStudent();

            _studentRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(student);
            _studentRepositoryMock.Setup(repo => repo.UpdateAsync(student)).ReturnsAsync(student);
            _mapperMock.Setup(mapper => mapper.Map<DetailedReadStudentOutput>(student)).Returns(new DetailedReadStudentOutput());

            // Act
            var result = await useCase.ExecuteAsync(id, input);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DetailedReadStudentOutput>(result);
            _studentRepositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            _studentRepositoryMock.Verify(repo => repo.UpdateAsync(student), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<DetailedReadStudentOutput>(student), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_NullId_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            Guid? id = null;
            var input = new UpdateStudentInput();

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
        }

        [Fact]
        public async Task ExecuteAsync_StudentNotFound_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var input = new UpdateStudentInput();

            _studentRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Domain.Entities.Student)null);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
            _studentRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Student>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_DeletedStudent_ThrowsUseCaseException()
        {
            // Arrange
            var useCase = CreateUseCase();
            var id = Guid.NewGuid();
            var input = new UpdateStudentInput();
            var student = StudentMock.MockValidStudent();
            student.DeactivateEntity();

            _studentRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(student);

            // Act and Assert
            await Assert.ThrowsAsync<UseCaseException>(async () => await useCase.ExecuteAsync(id, input));
            _studentRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Student>()), Times.Never);
        }
    }
}
