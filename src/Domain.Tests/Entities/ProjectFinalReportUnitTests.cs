using Domain.Entities;
using Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Entities
{
    public class ProjectFinalReportUnitTests
    {
        private static ProjectFinalReport MockValidProjectFinalReport()
        {
            return new ProjectFinalReport(Guid.NewGuid(), Guid.NewGuid())
            {
                ReportUrl = "https://example.com/report",
                SendDate = DateTime.UtcNow
            };
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SetReportUrl_NullOrEmptyValue_PropertyIsSet(string value)
        {
            // Arrange
            var projectFinalReport = MockValidProjectFinalReport();

            // Act
            projectFinalReport.ReportUrl = value;

            // Assert
            projectFinalReport.ReportUrl.Should().Be(value);
        }

        [Fact]
        public void SetSendDate_ValidValue_PropertyIsSet()
        {
            // Arrange
            var projectFinalReport = MockValidProjectFinalReport();
            var sendDate = DateTime.UtcNow;

            // Act
            projectFinalReport.SendDate = sendDate;

            // Assert
            projectFinalReport.SendDate.Should().Be(sendDate);
        }

        [Fact]
        public void SetSendDate_NullValue_ThrowsException()
        {
            // Arrange
            var projectFinalReport = MockValidProjectFinalReport();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => projectFinalReport.SendDate = null);
        }

        [Fact]
        public void SetProjectId_ValidValue_PropertyIsSet()
        {
            // Arrange
            var projectFinalReport = MockValidProjectFinalReport();
            var projectId = Guid.NewGuid();

            // Act
            projectFinalReport.ProjectId = projectId;

            // Assert
            projectFinalReport.ProjectId.Should().Be(projectId);
        }

        [Fact]
        public void SetProjectId_NullValue_ThrowsException()
        {
            // Arrange
            var projectFinalReport = MockValidProjectFinalReport();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => projectFinalReport.ProjectId = null);
        }

        [Fact]
        public void SetUserId_ValidValue_PropertyIsSet()
        {
            // Arrange
            var projectFinalReport = MockValidProjectFinalReport();
            var userId = Guid.NewGuid();

            // Act
            projectFinalReport.UserId = userId;

            // Assert
            projectFinalReport.UserId.Should().Be(userId);
        }

        [Fact]
        public void SetUserId_NullValue_ThrowsException()
        {
            // Arrange
            var projectFinalReport = MockValidProjectFinalReport();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => projectFinalReport.UserId = null);
        }

        [Fact]
        public void Constructor_ValidParameters_PropertiesSetCorrectly()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            // Act
            var projectFinalReport = new ProjectFinalReport(projectId, userId);

            // Assert
            projectFinalReport.ProjectId.Should().Be(projectId);
            projectFinalReport.SendDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }
    }
}
