using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Entities
{
    public class ProjectUnitTests
    {
        private Project MockValidProject()
        {
            var title = "Sample Title";
            var keyWord1 = "Keyword1";
            var keyWord2 = "Keyword2";
            var keyWord3 = "Keyword3";
            var isScholarshipCandidate = true;
            var objective = "Sample Objective";
            var methodology = "Sample Methodology";
            var expectedResults = "Sample Expected Results";
            var activitiesExecutionSchedule = "Sample Schedule";
            var workType1 = 1;
            var workType2 = 2;
            var indexedConferenceProceedings = 3;
            var notIndexedConferenceProceedings = 4;
            var completedBook = 5;
            var organizedBook = 6;
            var bookChapters = 7;
            var bookTranslations = 8;
            var participationEditorialCommittees = 9;
            var fullComposerSoloOrchestraAllTracks = 10;
            var fullComposerSoloOrchestraCompilation = 11;
            var chamberOrchestraInterpretation = 12;
            var individualAndCollectiveArtPerformances = 13;
            var scientificCulturalArtisticCollectionsCuratorship = 14;
            var patentLetter = 15;
            var patentDeposit = 16;
            var softwareRegistration = 17;
            var studentId = Guid.NewGuid();
            var programTypeId = Guid.NewGuid();
            var professorId = Guid.NewGuid();
            var subAreaId = Guid.NewGuid();
            var noticeId = Guid.NewGuid();
            var status = EProjectStatus.Accepted;
            var statusDescription = "Active project";
            var appealDescription = "Sample appeal description";
            var submissionDate = new DateTime(2023, 5, 30);
            var resubmissionDate = new DateTime(2023, 6, 10);
            var cancellationDate = new DateTime(2023, 6, 15);
            var cancellationReason = "Project is no longer feasible";

            return new Project(title, keyWord1, keyWord2, keyWord3, isScholarshipCandidate, objective, methodology,
                expectedResults, activitiesExecutionSchedule, workType1, workType2, indexedConferenceProceedings,
                notIndexedConferenceProceedings, completedBook, organizedBook, bookChapters, bookTranslations,
                participationEditorialCommittees, fullComposerSoloOrchestraAllTracks, fullComposerSoloOrchestraCompilation,
                chamberOrchestraInterpretation, individualAndCollectiveArtPerformances,
                scientificCulturalArtisticCollectionsCuratorship, patentLetter, patentDeposit, softwareRegistration,
                studentId, programTypeId, professorId, subAreaId, noticeId, status, statusDescription, appealDescription,
                submissionDate, resubmissionDate, cancellationDate, cancellationReason);
        }

        [Fact]
        public void SetTitle_ValidTitle_SetsTitle()
        {
            // Arrange
            var project = MockValidProject();
            var title = "Sample Project Title";

            // Act
            project.Title = title;

            // Assert
            project.Title.Should().Be(title);
        }

        [Fact]
        public void SetKeyword1_ValidKeyword1_SetsKeyword1()
        {
            // Arrange
            var project = MockValidProject();
            var keyword1 = "Keyword 1";

            // Act
            project.KeyWord1 = keyword1;

            // Assert
            project.KeyWord1.Should().Be(keyword1);
        }

        [Fact]
        public void SetKeyword2_ValidKeyword2_SetsKeyword2()
        {
            // Arrange
            var project = MockValidProject();
            var keyword2 = "Keyword 2";

            // Act
            project.KeyWord2 = keyword2;

            // Assert
            project.KeyWord2.Should().Be(keyword2);
        }

        [Fact]
        public void SetKeyword3_ValidKeyword3_SetsKeyword3()
        {
            // Arrange
            var project = MockValidProject();
            var keyword3 = "Keyword 3";

            // Act
            project.KeyWord3 = keyword3;

            // Assert
            project.KeyWord3.Should().Be(keyword3);
        }

        [Fact]
        public void SetIsScholarshipCandidate_ValidIsScholarshipCandidate_SetsIsScholarshipCandidate()
        {
            // Arrange
            var project = MockValidProject();
            var isScholarshipCandidate = true;

            // Act
            project.IsScholarshipCandidate = isScholarshipCandidate;

            // Assert
            project.IsScholarshipCandidate.Should().Be(isScholarshipCandidate);
        }

        [Fact]
        public void SetObjective_ValidObjective_SetsObjective()
        {
            // Arrange
            var project = MockValidProject();
            var objective = "Sample project objective.";

            // Act
            project.Objective = objective;

            // Assert
            project.Objective.Should().Be(objective);
        }

        [Fact]
        public void SetMethodology_ValidMethodology_SetsMethodology()
        {
            // Arrange
            var project = MockValidProject();
            var methodology = "Sample project methodology.";

            // Act
            project.Methodology = methodology;

            // Assert
            project.Methodology.Should().Be(methodology);
        }

        [Fact]
        public void SetExpectedResults_ValidExpectedResults_SetsExpectedResults()
        {
            // Arrange
            var project = MockValidProject();
            var expectedResults = "Sample expected project results.";

            // Act
            project.ExpectedResults = expectedResults;

            // Assert
            project.ExpectedResults.Should().Be(expectedResults);
        }

        [Fact]
        public void SetActivitiesExecutionSchedule_ValidActivitiesExecutionSchedule_SetsActivitiesExecutionSchedule()
        {
            // Arrange
            var project = MockValidProject();
            var activitiesExecutionSchedule = "Sample project activities execution schedule.";

            // Act
            project.ActivitiesExecutionSchedule = activitiesExecutionSchedule;

            // Assert
            project.ActivitiesExecutionSchedule.Should().Be(activitiesExecutionSchedule);
        }

        [Fact]
        public void SetWorkType1_ValidWorkType1_SetsWorkType1()
        {
            // Arrange
            var project = MockValidProject();
            var workType1 = 1;

            // Act
            project.WorkType1 = workType1;

            // Assert
            project.WorkType1.Should().Be(workType1);
        }

        [Fact]
        public void SetWorkType2_ValidWorkType2_SetsWorkType2()
        {
            // Arrange
            var project = MockValidProject();
            var workType2 = 15;

            // Act
            project.WorkType2 = workType2;

            // Assert
            project.WorkType2.Should().Be(workType2);
        }

        [Fact]
        public void SetIndexedConferenceProceedings_ValidIndexedConferenceProceedings_SetsIndexedConferenceProceedings()
        {
            // Arrange
            var project = MockValidProject();
            var indexedConferenceProceedings = 14;

            // Act
            project.IndexedConferenceProceedings = indexedConferenceProceedings;

            // Assert
            project.IndexedConferenceProceedings.Should().Be(indexedConferenceProceedings);
        }

        [Fact]
        public void SetNotIndexedConferenceProceedings_ValidNotIndexedConferenceProceedings_SetsNotIndexedConferenceProceedings()
        {
            // Arrange
            var project = MockValidProject();
            var notIndexedConferenceProceedings = 6;

            // Act
            project.NotIndexedConferenceProceedings = notIndexedConferenceProceedings;

            // Assert
            project.NotIndexedConferenceProceedings.Should().Be(notIndexedConferenceProceedings);
        }

        [Fact]
        public void SetCompletedBook_ValidCompletedBook_SetsCompletedBook()
        {
            // Arrange
            var project = MockValidProject();
            var completedBook = 3;

            // Act
            project.CompletedBook = completedBook;

            // Assert
            project.CompletedBook.Should().Be(completedBook);
        }

        [Fact]
        public void SetOrganizedBook_ValidOrganizedBook_SetsOrganizedBook()
        {
            // Arrange
            var project = MockValidProject();
            var organizedBook = 1;

            // Act
            project.OrganizedBook = organizedBook;

            // Assert
            project.OrganizedBook.Should().Be(organizedBook);
        }

        [Fact]
        public void SetBookChapters_ValidBookChapters_SetsBookChapters()
        {
            // Arrange
            var project = MockValidProject();
            var bookChapters = 13;

            // Act
            project.BookChapters = bookChapters;

            // Assert
            project.BookChapters.Should().Be(bookChapters);
        }

        [Fact]
        public void SetBookTranslations_ValidBookTranslations_SetsBookTranslations()
        {
            // Arrange
            var project = MockValidProject();
            var bookTranslations = 22;

            // Act
            project.BookTranslations = bookTranslations;

            // Assert
            project.BookTranslations.Should().Be(bookTranslations);
        }

        [Fact]
        public void SetParticipationEditorialCommittees_ValidParticipationEditorialCommittees_SetsParticipationEditorialCommittees()
        {
            // Arrange
            var project = MockValidProject();
            var participationEditorialCommittees = 11;

            // Act
            project.ParticipationEditorialCommittees = participationEditorialCommittees;

            // Assert
            project.ParticipationEditorialCommittees.Should().Be(participationEditorialCommittees);
        }

        [Fact]
        public void SetFullComposerSoloOrchestraAllTracks_ValidFullComposerSoloOrchestraAllTracks_SetsFullComposerSoloOrchestraAllTracks()
        {
            // Arrange
            var project = MockValidProject();
            var fullComposerSoloOrchestraAllTracks = 9;

            // Act
            project.FullComposerSoloOrchestraAllTracks = fullComposerSoloOrchestraAllTracks;

            // Assert
            project.FullComposerSoloOrchestraAllTracks.Should().Be(fullComposerSoloOrchestraAllTracks);
        }

        [Fact]
        public void SetFullComposerSoloOrchestraCompilation_ValidFullComposerSoloOrchestraCompilation_SetsFullComposerSoloOrchestraCompilation()
        {
            // Arrange
            var project = MockValidProject();
            var fullComposerSoloOrchestraCompilation = 8;

            // Act
            project.FullComposerSoloOrchestraCompilation = fullComposerSoloOrchestraCompilation;

            // Assert
            project.FullComposerSoloOrchestraCompilation.Should().Be(fullComposerSoloOrchestraCompilation);
        }

        [Fact]
        public void SetChamberOrchestraInterpretation_ValidChamberOrchestraInterpretation_SetsChamberOrchestraInterpretation()
        {
            // Arrange
            var project = MockValidProject();
            var chamberOrchestraInterpretation = 2;

            // Act
            project.ChamberOrchestraInterpretation = chamberOrchestraInterpretation;

            // Assert
            project.ChamberOrchestraInterpretation.Should().Be(chamberOrchestraInterpretation);
        }

        [Fact]
        public void SetIndividualAndCollectiveArtPerformances_ValidIndividualAndCollectiveArtPerformances_SetsIndividualAndCollectiveArtPerformances()
        {
            // Arrange
            var project = MockValidProject();
            var individualAndCollectiveArtPerformances = 7;

            // Act
            project.IndividualAndCollectiveArtPerformances = individualAndCollectiveArtPerformances;

            // Assert
            project.IndividualAndCollectiveArtPerformances.Should().Be(individualAndCollectiveArtPerformances);
        }

        [Fact]
        public void SetScientificCulturalArtisticCollectionsCuratorship_ValidScientificCulturalArtisticCollectionsCuratorship_SetsScientificCulturalArtisticCollectionsCuratorship()
        {
            // Arrange
            var project = MockValidProject();
            var scientificCulturalArtisticCollectionsCuratorship = 0;

            // Act
            project.ScientificCulturalArtisticCollectionsCuratorship = scientificCulturalArtisticCollectionsCuratorship;

            // Assert
            project.ScientificCulturalArtisticCollectionsCuratorship.Should().Be(scientificCulturalArtisticCollectionsCuratorship);
        }

        [Fact]
        public void SetPatentLetter_ValidPatentLetter_SetsPatentLetter()
        {
            // Arrange
            var project = MockValidProject();
            var patentLetter = 4;

            // Act
            project.PatentLetter = patentLetter;

            // Assert
            project.PatentLetter.Should().Be(patentLetter);
        }

        [Fact]
        public void SetPatentDeposit_ValidPatentDeposit_SetsPatentDeposit()
        {
            // Arrange
            var project = MockValidProject();
            var patentDeposit = 3;

            // Act
            project.PatentDeposit = patentDeposit;

            // Assert
            project.PatentDeposit.Should().Be(patentDeposit);
        }

        [Fact]
        public void SetSoftwareRegistration_ValidSoftwareRegistration_SetsSoftwareRegistration()
        {
            // Arrange
            var project = MockValidProject();
            var softwareRegistration = 2;

            // Act
            project.SoftwareRegistration = softwareRegistration;

            // Assert
            project.SoftwareRegistration.Should().Be(softwareRegistration);
        }

        [Fact]
        public void SetStudentId_ValidStudentId_SetsStudentId()
        {
            // Arrange
            var project = MockValidProject();
            var studentId = Guid.NewGuid();

            // Act
            project.StudentId = studentId;

            // Assert
            project.StudentId.Should().Be(studentId);
        }

        [Fact]
        public void SetProgramTypeId_ValidProgramTypeId_SetsProgramTypeId()
        {
            // Arrange
            var project = MockValidProject();
            var programTypeId = Guid.NewGuid();

            // Act
            project.ProgramTypeId = programTypeId;

            // Assert
            project.ProgramTypeId.Should().Be(programTypeId);
        }

        [Fact]
        public void SetSubAreaId_ValidSubAreaId_SetsSubAreaId()
        {
            // Arrange
            var project = MockValidProject();
            var subAreaId = Guid.NewGuid();

            // Act
            project.SubAreaId = subAreaId;

            // Assert
            project.SubAreaId.Should().Be(subAreaId);
        }

        [Fact]
        public void SetStatus_ValidStatus_SetsStatus()
        {
            // Arrange
            var project = MockValidProject();
            var status = EProjectStatus.Accepted;

            // Act
            project.Status = status;

            // Assert
            project.Status.Should().Be(status);
        }

        [Fact]
        public void SetStatusDescription_ValidStatusDescription_SetsStatusDescription()
        {
            // Arrange
            var project = MockValidProject();
            var statusDescription = EProjectStatus.Accepted.GetDescription();

            // Act
            project.StatusDescription = statusDescription;

            // Assert
            project.StatusDescription.Should().Be(statusDescription);
        }

        [Fact]
        public void SetAppealObservation_ValidAppealObservation_SetsAppealObservation()
        {
            // Arrange
            var project = MockValidProject();
            var appealDescription = "Sample appeal description.";

            // Act
            project.AppealObservation = appealDescription;

            // Assert
            project.AppealObservation.Should().Be(appealDescription);
        }

        [Fact]
        public void SetSubmissionDate_ValidSubmissionDate_SetsSubmissionDate()
        {
            // Arrange
            var project = MockValidProject();
            var submissionDate = DateTime.UtcNow;

            // Act
            project.SubmissionDate = submissionDate;

            // Assert
            project.SubmissionDate.Should().Be(submissionDate);
        }

        [Fact]
        public void SetResubmissionDate_ValidResubmissionDate_SetsResubmissionDate()
        {
            // Arrange
            var project = MockValidProject();
            var resubmissionDate = DateTime.UtcNow;

            // Act
            project.ResubmissionDate = resubmissionDate;

            // Assert
            project.ResubmissionDate.Should().Be(resubmissionDate);
        }

        [Fact]
        public void SetCancellationDate_ValidCancellationDate_SetsCancellationDate()
        {
            // Arrange
            var project = MockValidProject();
            var cancellationDate = DateTime.UtcNow;

            // Act
            project.CancellationDate = cancellationDate;

            // Assert
            project.CancellationDate.Should().Be(cancellationDate);
        }

        [Fact]
        public void SetCancellationReason_ValidCancellationReason_SetsCancellationReason()
        {
            // Arrange
            var project = MockValidProject();
            var cancellationReason = "Project is no longer feasible.";

            // Act
            project.CancellationReason = cancellationReason;

            // Assert
            project.CancellationReason.Should().Be(cancellationReason);
        }
    }
}