using Domain.Entities;
using Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Entities
{
    public class ActivityUnitTests
    {
        private static Activity MockValidActivity() =>
            new("Valid Name", 10.0, 20.0, Guid.NewGuid());

        [Fact]
        public void SetName_ValidName_SetsName()
        {
            // Arrange
            var activity = MockValidActivity();
            var name = "Valid New Name";

            // Act
            activity.Name = name;

            // Assert
            activity.Name.Should().Be(name);
        }

        [Theory]
        [InlineData("")]
        [InlineData("AB")]
        [InlineData("A")]
        [InlineData(null)]
        public void SetName_InvalidName_ThrowsException(string invalidName)
        {
            // Arrange
            var activity = MockValidActivity();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => activity.Name = invalidName);
        }

        [Fact]
        public void SetPoints_ValidPoints_SetsPoints()
        {
            // Arrange
            var activity = MockValidActivity();
            var points = 15.0;

            // Act
            activity.Points = points;

            // Assert
            activity.Points.Should().Be(points);
        }

        [Fact]
        public void SetPoints_NullPoints_ThrowsException()
        {
            // Arrange
            var activity = MockValidActivity();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => activity.Points = null);
        }

        [Fact]
        public void SetLimits_ValidLimits_SetsLimits()
        {
            // Arrange
            var activity = MockValidActivity();
            var limits = 25.0;

            // Act
            activity.Limits = limits;

            // Assert
            activity.Limits.Should().Be(limits);
        }

        [Fact]
        public void SetLimits_NullLimits_SetsLimitsToMaxValue()
        {
            // Arrange
            var activity = MockValidActivity();

            // Act
            activity.Limits = null;

            // Assert
            activity.Limits.Should().Be(double.MaxValue);
        }

        [Fact]
        public void Constructor_WithValidArguments_CreatesActivityWithCorrectValues()
        {
            // Arrange
            var name = "Valid Name";
            var points = 10.0;
            var limits = 20.0;
            var activityTypeId = Guid.NewGuid();

            // Act
            var activity = new Activity(name, points, limits, activityTypeId);

            // Assert
            activity.Name.Should().Be(name);
            activity.Points.Should().Be(points);
            activity.Limits.Should().Be(limits);
            activity.ActivityTypeId.Should().Be(activityTypeId);
        }

        [Fact]
        public void Constructor_WithNullName_ThrowsException()
        {
            // Arrange
            var name = null as string;
            var points = 10.0;
            var limits = 20.0;
            var activityTypeId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => new Activity(name, points, limits, activityTypeId));
        }

        [Fact]
        public void Constructor_WithNullPoints_ThrowsException()
        {
            // Arrange
            var name = "Valid Name";
            var points = (double?)null;
            var limits = 20.0;
            var activityTypeId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => new Activity(name, points, limits, activityTypeId));
        }

        [Fact]
        public void Constructor_WithNullActivityTypeId_ThrowsException()
        {
            // Arrange
            var name = "Valid Name";
            var points = 10.0;
            var limits = 20.0;
            var activityTypeId = (Guid?)null;

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => new Activity(name, points, limits, activityTypeId));
        }
    }
}
