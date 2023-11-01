using Domain.Entities;
using Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Entities
{
    public class ProjectActivityUnitTests
    {
        private static ProjectActivity MockValidProjectActivity() => new(Guid.NewGuid(), Guid.NewGuid(), 5, 3);

        [Fact]
        public void ProjectId_SetValidProjectId_PropertyIsSet()
        {
            // Arrange
            var projectActivity = MockValidProjectActivity();

            // Act
            projectActivity.ProjectId = Guid.NewGuid();

            // Assert
            projectActivity.ProjectId.Should().NotBeNull();
        }

        [Fact]
        public void ActivityId_SetValidActivityId_PropertyIsSet()
        {
            // Arrange
            var projectActivity = MockValidProjectActivity();

            // Act
            projectActivity.ActivityId = Guid.NewGuid();

            // Assert
            projectActivity.ActivityId.Should().NotBeNull();
        }

        [Fact]
        public void InformedActivities_SetValidInformedActivities_PropertyIsSet()
        {
            // Arrange
            var projectActivity = MockValidProjectActivity();

            // Act
            projectActivity.InformedActivities = 5;

            // Assert
            projectActivity.InformedActivities.Should().Be(5);
        }

        [Fact]
        public void FoundActivities_SetValidFoundActivities_PropertyIsSet()
        {
            // Arrange
            var projectActivity = MockValidProjectActivity();

            // Act
            projectActivity.FoundActivities = 3;

            // Assert
            projectActivity.FoundActivities.Should().Be(3);
        }

        [Fact]
        public void Constructor_ValidParameters_PropertiesSetCorrectly()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var activityId = Guid.NewGuid();
            var informedActivities = 5;
            var foundActivities = 3;

            // Act
            var projectActivity = new ProjectActivity(projectId, activityId, informedActivities, foundActivities);

            // Assert
            projectActivity.ProjectId.Should().Be(projectId);
            projectActivity.ActivityId.Should().Be(activityId);
            projectActivity.InformedActivities.Should().Be(informedActivities);
            projectActivity.FoundActivities.Should().Be(foundActivities);
        }

        [Fact]
        public void Constructor_NullProjectId_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() =>
                new ProjectActivity(null, Guid.NewGuid(), 5, 3));
        }

        [Fact]
        public void Constructor_NullActivityId_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() =>
                new ProjectActivity(Guid.NewGuid(), null, 5, 3));
        }

        [Fact]
        public void Constructor_NullInformedActivities_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() =>
                new ProjectActivity(Guid.NewGuid(), Guid.NewGuid(), null, 3));
        }

        [Fact]
        public void Constructor_NullFoundActivities_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() =>
                new ProjectActivity(Guid.NewGuid(), Guid.NewGuid(), 5, null));
        }

        [Fact]
        public void CalculatePoints_ActivityIsNull_ThrowsException()
        {
            // Arrange
            var projectActivity = new ProjectActivity(Guid.NewGuid(), Guid.NewGuid(), 5, 3);

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() =>
                projectActivity.CalculatePoints());
        }

        [Fact]
        public void CalculatePoints_ActivityHasNoLimits_ReturnsCorrectPoints()
        {
            // Arrange
            var activity = new Activity("Activity", 2, null, Guid.NewGuid());

            var projectActivity = new ProjectActivity(Guid.NewGuid(), Guid.NewGuid(), 5, 3);
            projectActivity.Activity = activity;

            // Act
            var points = projectActivity.CalculatePoints();

            // Assert
            points.Should().Be(6); // 2 points * 3 found activities
        }

        [Fact]
        public void CalculatePoints_ActivityHasLimits_ReturnsCorrectPoints()
        {
            // Arrange
            var activity = new Activity("Activity", 2, 5, Guid.NewGuid());

            var projectActivity = new ProjectActivity(Guid.NewGuid(), Guid.NewGuid(), 5, 3);
            projectActivity.Activity = activity;

            // Act
            var points = projectActivity.CalculatePoints();

            // Assert
            points.Should().Be(5); // Limited to 5 points
        }
    }
}
