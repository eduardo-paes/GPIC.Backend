using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Entities
{
    public class ProjectEvaluationUnitTests
    {
        private static ProjectEvaluation MockValidProjectEvaluation() =>
            new(
                Guid.NewGuid(),
                true,
                Guid.NewGuid(),
                EProjectStatus.Accepted,
                DateTime.UtcNow,
                "Valid Submission Evaluation Description",
                EQualification.Doctor,
                EScore.Excellent,
                EScore.Excellent,
                EScore.Excellent,
                EScore.Excellent,
                10.0);

        [Fact]
        public void Constructor_WithValidArguments_CreatesProjectEvaluationWithCorrectValues()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var isProductivityFellow = true;
            var submissionEvaluatorId = Guid.NewGuid();
            var submissionEvaluationStatus = EProjectStatus.Rejected;
            var submissionEvaluationDate = DateTime.UtcNow;
            var submissionEvaluationDescription = "Valid Submission Evaluation Description";
            var qualification = EQualification.Doctor;
            var projectProposalObjectives = EScore.Excellent;
            var academicScientificProductionCoherence = EScore.Excellent;
            var proposalMethodologyAdaptation = EScore.Excellent;
            var effectiveContributionToResearch = EScore.Excellent;
            var apIndex = 10.0;

            // Act
            var projectEvaluation = new ProjectEvaluation(
                projectId,
                isProductivityFellow,
                submissionEvaluatorId,
                submissionEvaluationStatus,
                submissionEvaluationDate,
                submissionEvaluationDescription,
                qualification,
                projectProposalObjectives,
                academicScientificProductionCoherence,
                proposalMethodologyAdaptation,
                effectiveContributionToResearch,
                apIndex);

            // Assert
            projectEvaluation.ProjectId.Should().Be(projectId);
            projectEvaluation.IsProductivityFellow.Should().Be(isProductivityFellow);
            projectEvaluation.SubmissionEvaluatorId.Should().Be(submissionEvaluatorId);
            projectEvaluation.SubmissionEvaluationStatus.Should().Be(submissionEvaluationStatus);
            projectEvaluation.SubmissionEvaluationDate.Should().Be(submissionEvaluationDate);
            projectEvaluation.SubmissionEvaluationDescription.Should().Be(submissionEvaluationDescription);
            projectEvaluation.Qualification.Should().Be(qualification);
            projectEvaluation.ProjectProposalObjectives.Should().Be(projectProposalObjectives);
            projectEvaluation.AcademicScientificProductionCoherence.Should().Be(academicScientificProductionCoherence);
            projectEvaluation.ProposalMethodologyAdaptation.Should().Be(proposalMethodologyAdaptation);
            projectEvaluation.EffectiveContributionToResearch.Should().Be(effectiveContributionToResearch);
            projectEvaluation.APIndex.Should().Be(apIndex);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Constructor_WithInvalidSubmissionEvaluationDescription_ThrowsException(string invalidDescription)
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var isProductivityFellow = true;
            var submissionEvaluatorId = Guid.NewGuid();
            var submissionEvaluationStatus = EProjectStatus.Rejected;
            var submissionEvaluationDate = DateTime.UtcNow;
            var qualification = EQualification.Doctor;
            var projectProposalObjectives = EScore.Excellent;
            var academicScientificProductionCoherence = EScore.Excellent;
            var proposalMethodologyAdaptation = EScore.Excellent;
            var effectiveContributionToResearch = EScore.Excellent;
            var apIndex = 10.0;

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => new ProjectEvaluation(
                projectId,
                isProductivityFellow,
                submissionEvaluatorId,
                submissionEvaluationStatus,
                submissionEvaluationDate,
                invalidDescription,
                qualification,
                projectProposalObjectives,
                academicScientificProductionCoherence,
                proposalMethodologyAdaptation,
                effectiveContributionToResearch,
                apIndex));
        }

        [Fact]
        public void Constructor_WithNullQualification_ThrowsException()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var isProductivityFellow = true;
            var submissionEvaluatorId = Guid.NewGuid();
            var submissionEvaluationStatus = EProjectStatus.Accepted;
            var submissionEvaluationDate = DateTime.UtcNow;
            var submissionEvaluationDescription = "Valid Submission Evaluation Description";
            EQualification? qualification = null;
            var projectProposalObjectives = EScore.Excellent;
            var academicScientificProductionCoherence = EScore.Excellent;
            var proposalMethodologyAdaptation = EScore.Excellent;
            var effectiveContributionToResearch = EScore.Excellent;
            var apIndex = 10.0;

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => new ProjectEvaluation(
                projectId,
                isProductivityFellow,
                submissionEvaluatorId,
                submissionEvaluationStatus,
                submissionEvaluationDate,
                submissionEvaluationDescription,
                qualification,
                projectProposalObjectives,
                academicScientificProductionCoherence,
                proposalMethodologyAdaptation,
                effectiveContributionToResearch,
                apIndex));
        }

        [Fact]
        public void CalculateFinalScore_WithValidScores_SetsFinalScore()
        {
            // Arrange
            var projectEvaluation = MockValidProjectEvaluation();
            projectEvaluation.Qualification = EQualification.Doctor;
            projectEvaluation.ProjectProposalObjectives = EScore.Excellent;
            projectEvaluation.AcademicScientificProductionCoherence = EScore.Good;
            projectEvaluation.ProposalMethodologyAdaptation = EScore.Regular;
            projectEvaluation.EffectiveContributionToResearch = EScore.Weak;
            projectEvaluation.APIndex = 8.0;

            // Act
            projectEvaluation.CalculateFinalScore();

            // Assert
            projectEvaluation.FinalScore.Should().BeApproximately(20.0, 0.001);
        }

        [Fact]
        public void SetProjectId_ValidId_SetsProjectId()
        {
            // Arrange
            var projectEvaluation = MockValidProjectEvaluation();
            var projectId = Guid.NewGuid();

            // Act
            projectEvaluation.ProjectId = projectId;

            // Assert
            projectEvaluation.ProjectId.Should().Be(projectId);
        }

        [Fact]
        public void SetProjectId_NullId_ThrowsException()
        {
            // Arrange
            var projectEvaluation = MockValidProjectEvaluation();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => projectEvaluation.ProjectId = null);
        }

        [Fact]
        public void SetIsProductivityFellow_ValidValue_SetsIsProductivityFellow()
        {
            // Arrange
            var projectEvaluation = MockValidProjectEvaluation();
            var isProductivityFellow = true;

            // Act
            projectEvaluation.IsProductivityFellow = isProductivityFellow;

            // Assert
            projectEvaluation.IsProductivityFellow.Should().Be(isProductivityFellow);
        }

        [Fact]
        public void SetIsProductivityFellow_NullValue_ThrowsException()
        {
            // Arrange
            var projectEvaluation = MockValidProjectEvaluation();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => projectEvaluation.IsProductivityFellow = null);
        }

        [Fact]
        public void SetSubmissionEvaluatorId_ValidId_SetsSubmissionEvaluatorId()
        {
            // Arrange
            var projectEvaluation = MockValidProjectEvaluation();
            var submissionEvaluatorId = Guid.NewGuid();

            // Act
            projectEvaluation.SubmissionEvaluatorId = submissionEvaluatorId;

            // Assert
            projectEvaluation.SubmissionEvaluatorId.Should().Be(submissionEvaluatorId);
        }

        [Fact]
        public void SetSubmissionEvaluatorId_NullId_ThrowsException()
        {
            // Arrange
            var projectEvaluation = MockValidProjectEvaluation();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => projectEvaluation.SubmissionEvaluatorId = null);
        }

        [Fact]
        public void SetSubmissionEvaluationDate_ValidDate_SetsSubmissionEvaluationDate()
        {
            // Arrange
            var projectEvaluation = MockValidProjectEvaluation();
            var evaluationDate = DateTime.Now;

            // Act
            projectEvaluation.SubmissionEvaluationDate = evaluationDate;

            // Assert
            projectEvaluation.SubmissionEvaluationDate.Should().Be(evaluationDate);
        }

        [Fact]
        public void SetSubmissionEvaluationDate_NullDate_ThrowsException()
        {
            // Arrange
            var projectEvaluation = MockValidProjectEvaluation();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => projectEvaluation.SubmissionEvaluationDate = null);
        }

        [Fact]
        public void SetSubmissionEvaluationStatus_ValidStatus_SetsSubmissionEvaluationStatus()
        {
            // Arrange
            var projectEvaluation = MockValidProjectEvaluation();
            var evaluationStatus = EProjectStatus.Accepted;

            // Act
            projectEvaluation.SubmissionEvaluationStatus = evaluationStatus;

            // Assert
            projectEvaluation.SubmissionEvaluationStatus.Should().Be(evaluationStatus);
        }

        [Fact]
        public void SetSubmissionEvaluationStatus_NullStatus_ThrowsException()
        {
            // Arrange
            var projectEvaluation = MockValidProjectEvaluation();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => projectEvaluation.SubmissionEvaluationStatus = null);
        }

        [Fact]
        public void SetSubmissionEvaluationDescription_ValidDescription_SetsSubmissionEvaluationDescription()
        {
            // Arrange
            var projectEvaluation = MockValidProjectEvaluation();
            var description = "Accepted";

            // Act
            projectEvaluation.SubmissionEvaluationDescription = description;

            // Assert
            projectEvaluation.SubmissionEvaluationDescription.Should().Be(description);
        }

        [Fact]
        public void SetSubmissionEvaluationDescription_NullDescription_ThrowsException()
        {
            // Arrange
            var projectEvaluation = MockValidProjectEvaluation();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => projectEvaluation.SubmissionEvaluationDescription = null);
        }

        [Fact]
        public void SetAppealEvaluatorId_ValidId_SetsAppealEvaluatorId()
        {
            // Arrange
            var projectEvaluation = MockValidProjectEvaluation();
            var appealEvaluatorId = Guid.NewGuid();

            // Act
            projectEvaluation.AppealEvaluatorId = appealEvaluatorId;

            // Assert
            projectEvaluation.AppealEvaluatorId.Should().Be(appealEvaluatorId);
        }

        [Fact]
        public void SetAppealEvaluationDate_ValidDate_SetsAppealEvaluationDate()
        {
            // Arrange
            var projectEvaluation = MockValidProjectEvaluation();
            var evaluationDate = DateTime.Now;

            // Act
            projectEvaluation.AppealEvaluationDate = evaluationDate;

            // Assert
            projectEvaluation.AppealEvaluationDate.Should().Be(evaluationDate);
        }

        [Fact]
        public void SetAppealEvaluationStatus_ValidStatus_SetsAppealEvaluationStatus()
        {
            // Arrange
            var projectEvaluation = MockValidProjectEvaluation();
            var evaluationStatus = EProjectStatus.Rejected;

            // Act
            projectEvaluation.AppealEvaluationStatus = evaluationStatus;

            // Assert
            projectEvaluation.AppealEvaluationStatus.Should().Be(evaluationStatus);
        }

        [Fact]
        public void SetAppealEvaluationDescription_ValidDescription_SetsAppealEvaluationDescription()
        {
            // Arrange
            var projectEvaluation = MockValidProjectEvaluation();
            var description = "Rejected due to insufficient details.";

            // Act
            projectEvaluation.AppealEvaluationDescription = description;

            // Assert
            projectEvaluation.AppealEvaluationDescription.Should().Be(description);
        }
    }
}
