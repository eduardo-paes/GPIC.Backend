using Domain.Entities;
using Domain.Validation;
using FluentAssertions;
using System;
using Xunit;

namespace Domain.Tests.Entities
{
    public class NoticeUnitTests
    {
        private Notice MockValidNotice() => new Notice();

        [Fact]
        public void SetStartDate_ValidStartDate_SetsStartDate()
        {
            // Arrange
            var notice = MockValidNotice();
            var startDate = DateTime.Now;

            // Act
            notice.StartDate = startDate;

            // Assert
            notice.StartDate.Should().Be(startDate.ToUniversalTime());
        }

        [Fact]
        public void SetStartDate_NullStartDate_ThrowsException()
        {
            // Arrange
            var notice = MockValidNotice();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => notice.StartDate = null);
        }

        [Fact]
        public void SetFinalDate_ValidFinalDate_SetsFinalDate()
        {
            // Arrange
            var notice = MockValidNotice();
            var finalDate = DateTime.Now;

            // Act
            notice.FinalDate = finalDate;

            // Assert
            notice.FinalDate.Should().Be(finalDate.ToUniversalTime());
        }

        [Fact]
        public void SetFinalDate_NullFinalDate_ThrowsException()
        {
            // Arrange
            var notice = MockValidNotice();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => notice.FinalDate = null);
        }

        [Fact]
        public void SetAppealStartDate_ValidAppealStartDate_SetsAppealStartDate()
        {
            // Arrange
            var notice = MockValidNotice();
            var appealStartDate = DateTime.Now;

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
        public void SetAppealFinalDate_ValidAppealFinalDate_SetsAppealFinalDate()
        {
            // Arrange
            var notice = MockValidNotice();
            var appealFinalDate = DateTime.Now;

            // Act
            notice.AppealFinalDate = appealFinalDate;

            // Assert
            notice.AppealFinalDate.Should().Be(appealFinalDate.ToUniversalTime());
        }

        [Fact]
        public void SetAppealFinalDate_NullAppealFinalDate_ThrowsException()
        {
            // Arrange
            var notice = MockValidNotice();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => notice.AppealFinalDate = null);
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
        public void SetSendingDocumentationDeadline_ValidSendingDocumentationDeadline_SetsSendingDocumentationDeadline()
        {
            // Arrange
            var notice = MockValidNotice();
            var sendingDocumentationDeadline = 1;

            // Act
            notice.SendingDocumentationDeadline = sendingDocumentationDeadline;

            // Assert
            notice.SendingDocumentationDeadline.Should().Be(sendingDocumentationDeadline);
        }

        [Fact]
        public void SetSendingDocumentationDeadline_NullSendingDocumentationDeadline_ThrowsException()
        {
            // Arrange
            var notice = MockValidNotice();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => notice.SendingDocumentationDeadline = null);
        }

        [Fact]
        public void SetSendingDocumentationDeadline_NegativeSendingDocumentationDeadline_ThrowsException()
        {
            // Arrange
            var notice = MockValidNotice();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => notice.SendingDocumentationDeadline = -1);
        }
    }
}
