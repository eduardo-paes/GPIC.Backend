using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Validation;
using FluentAssertions;
using System;
using Xunit;

namespace Domain.Tests.Entities
{
    public class ProjectEvaluationUnitTests
    {
        private ProjectEvaluation MockValidProjectEvaluation()
        {
            return new ProjectEvaluation
            {
                ProjectId = Guid.NewGuid(),
                IsProductivityFellow = true,
                SubmissionEvaluatorId = Guid.NewGuid(),
                SubmissionEvaluationDate = DateTime.Now,
                SubmissionEvaluationStatus = EProjectStatus.Pending,
                SubmissionEvaluationDescription = "Submission evaluation description",
                AppealEvaluatorId = Guid.NewGuid(),
                AppealEvaluationDate = DateTime.Now,
                AppealEvaluationStatus = EProjectStatus.Accepted,
                AppealEvaluationDescription = "Appeal evaluation description",
                DocumentsEvaluatorId = Guid.NewGuid(),
                DocumentsEvaluationDate = DateTime.Now,
                DocumentsEvaluationDescription = "Documents evaluation description",
                FoundWorkType1 = 1,
                FoundWorkType2 = 2,
                FoundIndexedConferenceProceedings = 3,
                FoundNotIndexedConferenceProceedings = 4,
                FoundCompletedBook = 5,
                FoundOrganizedBook = 6,
                FoundBookChapters = 7,
                FoundBookTranslations = 8,
                FoundParticipationEditorialCommittees = 9,
                FoundFullComposerSoloOrchestraAllTracks = 10,
                FoundFullComposerSoloOrchestraCompilation = 11,
                FoundChamberOrchestraInterpretation = 12,
                FoundIndividualAndCollectiveArtPerformances = 13,
                FoundScientificCulturalArtisticCollectionsCuratorship = 14,
                FoundPatentLetter = 15,
                FoundPatentDeposit = 16,
                FoundSoftwareRegistration = 17,
                APIndex = 18,
                Qualification = EQualification.Master,
                ProjectProposalObjectives = EScore.Good,
                AcademicScientificProductionCoherence = EScore.Excellent,
                ProposalMethodologyAdaptation = EScore.Regular,
                EffectiveContributionToResearch = EScore.Weak
            };
        }

        [Fact]
        public void TestAllProperties()
        {
            // Arrange
            var projectEvaluation = MockValidProjectEvaluation();

            // Act & Assert
            AssertValidation(() => projectEvaluation.ProjectId = null);
            AssertValidation(() => projectEvaluation.IsProductivityFellow = null);
            AssertValidation(() => projectEvaluation.SubmissionEvaluatorId = null);
            AssertValidation(() => projectEvaluation.SubmissionEvaluationDate = null);
            AssertValidation(() => projectEvaluation.SubmissionEvaluationStatus = null);
            AssertValidation(() => projectEvaluation.SubmissionEvaluationDescription = null);
            // AssertValidation(() => projectEvaluation.AppealEvaluatorId = null);
            // AssertValidation(() => projectEvaluation.AppealEvaluationDate = null);
            // AssertValidation(() => projectEvaluation.AppealEvaluationStatus = null);
            // AssertValidation(() => projectEvaluation.AppealEvaluationDescription = null);
            // AssertValidation(() => projectEvaluation.DocumentsEvaluatorId = null);
            // AssertValidation(() => projectEvaluation.DocumentsEvaluationDate = null);
            // AssertValidation(() => projectEvaluation.DocumentsEvaluationDescription = null);
            AssertValidation(() => projectEvaluation.FoundWorkType1 = null);
            AssertValidation(() => projectEvaluation.FoundWorkType2 = null);
            AssertValidation(() => projectEvaluation.FoundIndexedConferenceProceedings = null);
            AssertValidation(() => projectEvaluation.FoundNotIndexedConferenceProceedings = null);
            AssertValidation(() => projectEvaluation.FoundCompletedBook = null);
            AssertValidation(() => projectEvaluation.FoundOrganizedBook = null);
            AssertValidation(() => projectEvaluation.FoundBookChapters = null);
            AssertValidation(() => projectEvaluation.FoundBookTranslations = null);
            AssertValidation(() => projectEvaluation.FoundParticipationEditorialCommittees = null);
            AssertValidation(() => projectEvaluation.FoundFullComposerSoloOrchestraAllTracks = null);
            AssertValidation(() => projectEvaluation.FoundFullComposerSoloOrchestraCompilation = null);
            AssertValidation(() => projectEvaluation.FoundChamberOrchestraInterpretation = null);
            AssertValidation(() => projectEvaluation.FoundIndividualAndCollectiveArtPerformances = null);
            AssertValidation(() => projectEvaluation.FoundScientificCulturalArtisticCollectionsCuratorship = null);
            AssertValidation(() => projectEvaluation.FoundPatentLetter = null);
            AssertValidation(() => projectEvaluation.FoundPatentDeposit = null);
            AssertValidation(() => projectEvaluation.FoundSoftwareRegistration = null);
            AssertValidation(() => projectEvaluation.APIndex = null);
            AssertValidation(() => projectEvaluation.Qualification = null);
            AssertValidation(() => projectEvaluation.ProjectProposalObjectives = null);
            AssertValidation(() => projectEvaluation.AcademicScientificProductionCoherence = null);
            AssertValidation(() => projectEvaluation.ProposalMethodologyAdaptation = null);
            AssertValidation(() => projectEvaluation.EffectiveContributionToResearch = null);
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
            var submissionEvaluationDate = DateTime.UtcNow;

            // Act
            projectEvaluation.SubmissionEvaluationDate = submissionEvaluationDate;

            // Assert
            projectEvaluation.SubmissionEvaluationDate.Should().Be(submissionEvaluationDate);
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
            var submissionEvaluationStatus = EProjectStatus.Accepted;

            // Act
            projectEvaluation.SubmissionEvaluationStatus = submissionEvaluationStatus;

            // Assert
            projectEvaluation.SubmissionEvaluationStatus.Should().Be(submissionEvaluationStatus);
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
            var submissionEvaluationDescription = "This is a valid description.";

            // Act
            projectEvaluation.SubmissionEvaluationDescription = submissionEvaluationDescription;

            // Assert
            projectEvaluation.SubmissionEvaluationDescription.Should().Be(submissionEvaluationDescription);
        }

        [Fact]
        public void SetSubmissionEvaluationDescription_NullDescription_ThrowsException()
        {
            // Arrange
            var projectEvaluation = MockValidProjectEvaluation();

            // Act & Assert
            Assert.Throws<EntityExceptionValidation>(() => projectEvaluation.SubmissionEvaluationDescription = null);
        }

        private void AssertValidation(Action action)
        {
            Assert.Throws<EntityExceptionValidation>(action);
        }
    }
}
