using Domain.Entities;
using Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Entities
{
    public class StudentDocumentsUnitTests
    {
        private static StudentDocuments MockValidStudentDocuments()
        {
            return new StudentDocuments(Guid.NewGuid(), "1234", "5678")
            {
                IdentityDocument = "123456789",
                CPF = "12345678901",
                Photo3x4 = "PhotoData",
                SchoolHistory = "SchoolHistoryData",
                ScholarCommitmentAgreement = "ScholarCommitmentData",
                ParentalAuthorization = "ParentalAuthorizationData"
            };
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SetIdentityDocument_NullOrEmptyValue_ThrowsException(string value)
        {
            // Arrange
            var studentDocuments = MockValidStudentDocuments();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => studentDocuments.IdentityDocument = value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SetCPF_NullOrEmptyValue_ThrowsException(string value)
        {
            // Arrange
            var studentDocuments = MockValidStudentDocuments();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => studentDocuments.CPF = value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SetPhoto3x4_NullOrEmptyValue_ThrowsException(string value)
        {
            // Arrange
            var studentDocuments = MockValidStudentDocuments();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => studentDocuments.Photo3x4 = value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SetSchoolHistory_NullOrEmptyValue_ThrowsException(string value)
        {
            // Arrange
            var studentDocuments = MockValidStudentDocuments();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => studentDocuments.SchoolHistory = value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SetScholarCommitmentAgreement_NullOrEmptyValue_ThrowsException(string value)
        {
            // Arrange
            var studentDocuments = MockValidStudentDocuments();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => studentDocuments.ScholarCommitmentAgreement = value);
        }

        // [Theory]
        // [InlineData(null)]
        // [InlineData("")]
        // public void SetParentalAuthorization_NullOrEmptyValue_ThrowsException(string value)
        // {
        //     // Arrange
        //     var studentDocuments = MockValidStudentDocuments();

        //     studentDocuments.Project = new Mock<Project>();

        //     // Act & Assert
        //     Assert.Throws<EntityExceptionValidation>(() => studentDocuments.ParentalAuthorization = value);
        // }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("0")]
        public void SetAgencyNumber_InvalidValue_ThrowsException(string agencyNumber)
        {
            // Arrange
            var studentDocuments = MockValidStudentDocuments();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => studentDocuments.AgencyNumber = agencyNumber);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("0")]
        public void SetAccountNumber_InvalidValue_ThrowsException(string accountNumber)
        {
            // Arrange
            var studentDocuments = MockValidStudentDocuments();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => studentDocuments.AccountNumber = accountNumber);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SetAccountOpeningProof_NullOrEmptyValue_ThrowsException(string value)
        {
            // Arrange
            var studentDocuments = MockValidStudentDocuments();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => studentDocuments.AccountOpeningProof = value);
        }

        [Fact]
        public void Constructor_ValidParameters_PropertiesSetCorrectly()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var agencyNumber = "1234";
            var accountNumber = "5678";

            // Act
            var studentDocuments = new StudentDocuments(projectId, agencyNumber, accountNumber);

            // Assert
            studentDocuments.ProjectId.Should().Be(projectId);
            studentDocuments.AgencyNumber.Should().Be(agencyNumber);
            studentDocuments.AccountNumber.Should().Be(accountNumber);
        }
    }
}
