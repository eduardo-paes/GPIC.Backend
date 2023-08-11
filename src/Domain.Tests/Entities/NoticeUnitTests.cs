using Domain.Entities;
using Domain.Validation;
using FluentAssertions;
using System;
using Xunit;

namespace Domain.Tests.Entities
{
    public class NoticeUnitTests
    {
        private static Notice MockValidNotice() => new(
            registrationStartDate: DateTime.UtcNow,
            registrationEndDate: DateTime.UtcNow.AddDays(7),
            evaluationStartDate: DateTime.UtcNow.AddDays(8),
            evaluationEndDate: DateTime.UtcNow.AddDays(14),
            appealStartDate: DateTime.UtcNow.AddDays(15),
            appealFinalDate: DateTime.UtcNow.AddDays(21),
            sendingDocsStartDate: DateTime.UtcNow.AddDays(22),
            sendingDocsEndDate: DateTime.UtcNow.AddDays(28),
            partialReportDeadline: DateTime.UtcNow.AddDays(29),
            finalReportDeadline: DateTime.UtcNow.AddDays(35),
            suspensionYears: 1
        );

        [Fact]
        public void SetRegistrationStartDate_ValidStartDate_SetsRegistrationStartDate()
        {
            // Arrange
            var notice = MockValidNotice();
            var startDate = DateTime.UtcNow;

            // Act
            notice.RegistrationStartDate = startDate;

            // Assert
            notice.RegistrationStartDate.Should().Be(startDate.ToUniversalTime());
        }

        [Fact]
        public void SetRegistrationStartDate_NullStartDate_ThrowsException()
        {
            // Arrange
            var notice = MockValidNotice();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => notice.RegistrationStartDate = null);
        }

        [Fact]
        public void SetRegistrationEndDate_ValidEndDate_SetsRegistrationEndDate()
        {
            // Arrange
            var notice = MockValidNotice();
            var endDate = DateTime.UtcNow;

            // Act
            notice.RegistrationEndDate = endDate;

            // Assert
            notice.RegistrationEndDate.Should().Be(endDate.ToUniversalTime());
        }

        [Fact]
        public void SetRegistrationEndDate_NullEndDate_ThrowsException()
        {
            // Arrange
            var notice = MockValidNotice();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => notice.RegistrationEndDate = null);
        }

        [Fact]
        public void SetEvaluationStartDate_ValidStartDate_SetsEvaluationStartDate()
        {
            // Arrange
            var notice = MockValidNotice();
            var startDate = DateTime.UtcNow;

            // Act
            notice.EvaluationStartDate = startDate;

            // Assert
            notice.EvaluationStartDate.Should().Be(startDate.ToUniversalTime());
        }

        [Fact]
        public void SetEvaluationStartDate_NullStartDate_ThrowsException()
        {
            // Arrange
            var notice = MockValidNotice();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => notice.EvaluationStartDate = null);
        }

        [Fact]
        public void SetEvaluationEndDate_ValidEndDate_SetsEvaluationEndDate()
        {
            // Arrange
            var notice = MockValidNotice();
            var endDate = DateTime.UtcNow;

            // Act
            notice.EvaluationEndDate = endDate;

            // Assert
            notice.EvaluationEndDate.Should().Be(endDate.ToUniversalTime());
        }

        [Fact]
        public void SetEvaluationEndDate_NullEndDate_ThrowsException()
        {
            // Arrange
            var notice = MockValidNotice();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => notice.EvaluationEndDate = null);
        }

        [Fact]
        public void SetAppealStartDate_ValidAppealStartDate_SetsAppealStartDate()
        {
            // Arrange
            var notice = MockValidNotice();
            var appealStartDate = DateTime.UtcNow;

            // Act
            notice.AppealStartDate = appealStartDate;

            // Assert
            notice.AppealStartDate.Should().Be(appealStartDate.ToUniversalTime());
        }

        [Fact]
        public void SetAppealStartDate_NullAppealStartDate_ThrowsException()
        {
            // Arrange
            var notice = MockValidNotice();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => notice.AppealStartDate = null);
        }

        [Fact]
        public void SetAppealEndDate_ValidAppealEndDate_SetsAppealEndDate()
        {
            // Arrange
            var notice = MockValidNotice();
            var appealEndDate = DateTime.UtcNow;

            // Act
            notice.AppealEndDate = appealEndDate;

            // Assert
            notice.AppealEndDate.Should().Be(appealEndDate.ToUniversalTime());
        }

        [Fact]
        public void SetAppealEndDate_NullAppealEndDate_ThrowsException()
        {
            // Arrange
            var notice = MockValidNotice();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => notice.AppealEndDate = null);
        }

        [Fact]
        public void SetSendingDocsStartDate_ValidStartDate_SetsSendingDocsStartDate()
        {
            // Arrange
            var notice = MockValidNotice();
            var startDate = DateTime.UtcNow;

            // Act
            notice.SendingDocsStartDate = startDate;

            // Assert
            notice.SendingDocsStartDate.Should().Be(startDate.ToUniversalTime());
        }

        [Fact]
        public void SetSendingDocsStartDate_NullStartDate_ThrowsException()
        {
            // Arrange
            var notice = MockValidNotice();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => notice.SendingDocsStartDate = null);
        }

        [Fact]
        public void SetSendingDocsEndDate_ValidEndDate_SetsSendingDocsEndDate()
        {
            // Arrange
            var notice = MockValidNotice();
            var endDate = DateTime.UtcNow;

            // Act
            notice.SendingDocsEndDate = endDate;

            // Assert
            notice.SendingDocsEndDate.Should().Be(endDate.ToUniversalTime());
        }

        [Fact]
        public void SetSendingDocsEndDate_NullEndDate_ThrowsException()
        {
            // Arrange
            var notice = MockValidNotice();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => notice.SendingDocsEndDate = null);
        }

        [Fact]
        public void SetPartialReportDeadline_ValidDeadline_SetsPartialReportDeadline()
        {
            // Arrange
            var notice = MockValidNotice();
            var deadline = DateTime.UtcNow;

            // Act
            notice.PartialReportDeadline = deadline;

            // Assert
            notice.PartialReportDeadline.Should().Be(deadline.ToUniversalTime());
        }

        [Fact]
        public void SetPartialReportDeadline_NullDeadline_ThrowsException()
        {
            // Arrange
            var notice = MockValidNotice();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => notice.PartialReportDeadline = null);
        }

        [Fact]
        public void SetFinalReportDeadline_ValidDeadline_SetsFinalReportDeadline()
        {
            // Arrange
            var notice = MockValidNotice();
            var deadline = DateTime.UtcNow;

            // Act
            notice.FinalReportDeadline = deadline;

            // Assert
            notice.FinalReportDeadline.Should().Be(deadline.ToUniversalTime());
        }

        [Fact]
        public void SetFinalReportDeadline_NullDeadline_ThrowsException()
        {
            // Arrange
            var notice = MockValidNotice();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => notice.FinalReportDeadline = null);
        }

        [Fact]
        public void SetSuspensionYears_ValidSuspensionYears_SetsSuspensionYears()
        {
            // Arrange
            var notice = MockValidNotice();
            var suspensionYears = 1;

            // Act
            notice.SuspensionYears = suspensionYears;

            // Assert
            notice.SuspensionYears.Should().Be(suspensionYears);
        }

        [Fact]
        public void SetSuspensionYears_NullSuspensionYears_ThrowsException()
        {
            // Arrange
            var notice = MockValidNotice();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => notice.SuspensionYears = null);
        }

        [Fact]
        public void SetSuspensionYears_NegativeSuspensionYears_ThrowsException()
        {
            // Arrange
            var notice = MockValidNotice();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => notice.SuspensionYears = -1);
        }

        [Fact]
        public void SetDocUrl_ValidUrl_SetsDocUrl()
        {
            // Arrange
            var notice = MockValidNotice();
            var url = "https://www.example.com";

            // Act
            notice.DocUrl = url;

            // Assert
            notice.DocUrl.Should().Be(url);
        }

        // Add additional tests for DocUrl property (e.g., invalid URLs).

        [Fact]
        public void SetCreatedAt_ValidDate_SetsCreatedAt()
        {
            // Arrange
            var notice = MockValidNotice();

            // Assert
            notice.CreatedAt.Should().NotBeNull();
        }
    }
}