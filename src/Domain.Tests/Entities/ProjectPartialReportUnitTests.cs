using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Entities
{
    public class ProjectPartialReportUnitTests
    {
        private static ProjectPartialReport MockValidProjectPartialReport()
        {
            return new ProjectPartialReport(Guid.NewGuid(), 50, EScholarPerformance.Good, "AdditionalInfo", Guid.NewGuid());
        }

        [Fact]
        public void SetCurrentDevelopmentStage_ValidValue_PropertyIsSet()
        {
            // Arrange
            var projectPartialReport = MockValidProjectPartialReport();
            var currentDevelopmentStage = 75;

            // Act
            projectPartialReport.CurrentDevelopmentStage = currentDevelopmentStage;

            // Assert
            projectPartialReport.CurrentDevelopmentStage.Should().Be(currentDevelopmentStage);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(101)]
        public void SetCurrentDevelopmentStage_InvalidValue_ThrowsException(int value)
        {
            // Arrange
            var projectPartialReport = MockValidProjectPartialReport();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => projectPartialReport.CurrentDevelopmentStage = value);
        }

        [Theory]
        [InlineData(EScholarPerformance.Bad)]
        [InlineData(EScholarPerformance.Regular)]
        [InlineData(EScholarPerformance.Good)]
        [InlineData(EScholarPerformance.VeryGood)]
        [InlineData(EScholarPerformance.Excellent)]
        public void SetScholarPerformance_ValidValue_PropertyIsSet(EScholarPerformance value)
        {
            // Arrange
            var projectPartialReport = MockValidProjectPartialReport();

            // Act
            projectPartialReport.ScholarPerformance = value;

            // Assert
            projectPartialReport.ScholarPerformance.Should().Be(value);
        }

        [Fact]
        public void SetScholarPerformance_NullValue_ThrowsException()
        {
            // Arrange
            var projectPartialReport = MockValidProjectPartialReport();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => projectPartialReport.ScholarPerformance = null);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SetAdditionalInfo_NullOrEmptyValue_PropertyIsSet(string value)
        {
            // Arrange
            var projectPartialReport = MockValidProjectPartialReport();

            // Act
            projectPartialReport.AdditionalInfo = value;

            // Assert
            projectPartialReport.AdditionalInfo.Should().Be(value);
        }

        [Fact]
        public void SetProjectId_ValidValue_PropertyIsSet()
        {
            // Arrange
            var projectPartialReport = MockValidProjectPartialReport();
            var projectId = Guid.NewGuid();

            // Act
            projectPartialReport.ProjectId = projectId;

            // Assert
            projectPartialReport.ProjectId.Should().Be(projectId);
        }

        [Fact]
        public void SetProjectId_NullValue_ThrowsException()
        {
            // Arrange
            var projectPartialReport = MockValidProjectPartialReport();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => projectPartialReport.ProjectId = null);
        }

        [Fact]
        public void SetUserId_ValidValue_PropertyIsSet()
        {
            // Arrange
            var projectPartialReport = MockValidProjectPartialReport();
            var userId = Guid.NewGuid();

            // Act
            projectPartialReport.UserId = userId;

            // Assert
            projectPartialReport.UserId.Should().Be(userId);
        }

        [Fact]
        public void SetUserId_NullValue_ThrowsException()
        {
            // Arrange
            var projectPartialReport = MockValidProjectPartialReport();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => projectPartialReport.UserId = null);
        }

        [Fact]
        public void Constructor_ValidParameters_PropertiesSetCorrectly()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var currentDevelopmentStage = 50;
            var scholarPerformance = EScholarPerformance.Good;
            var additionalInfo = "AdditionalInfo";
            var userId = Guid.NewGuid();

            // Act
            var projectPartialReport = new ProjectPartialReport(projectId, currentDevelopmentStage, scholarPerformance, additionalInfo, userId);

            // Assert
            projectPartialReport.ProjectId.Should().Be(projectId);
            projectPartialReport.CurrentDevelopmentStage.Should().Be(currentDevelopmentStage);
            projectPartialReport.ScholarPerformance.Should().Be(scholarPerformance);
            projectPartialReport.AdditionalInfo.Should().Be(additionalInfo);
            projectPartialReport.UserId.Should().Be(userId);
        }
    }
}
