using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntitiesConfiguration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Title);
            builder.Property(p => p.KeyWord1).HasMaxLength(100);
            builder.Property(p => p.KeyWord2).HasMaxLength(100);
            builder.Property(p => p.KeyWord3).HasMaxLength(100);
            builder.Property(p => p.IsScholarshipCandidate);
            builder.Property(p => p.Objective).HasMaxLength(1500);
            builder.Property(p => p.Methodology).HasMaxLength(1500);
            builder.Property(p => p.ExpectedResults).HasMaxLength(1500);
            builder.Property(p => p.ActivitiesExecutionSchedule).HasMaxLength(1500);
            builder.Property(p => p.WorkType1).HasDefaultValue(0);
            builder.Property(p => p.WorkType2).HasDefaultValue(0);
            builder.Property(p => p.IndexedConferenceProceedings).HasDefaultValue(0);
            builder.Property(p => p.NotIndexedConferenceProceedings).HasDefaultValue(0);
            builder.Property(p => p.CompletedBook).HasDefaultValue(0);
            builder.Property(p => p.OrganizedBook).HasDefaultValue(0);
            builder.Property(p => p.BookChapters).HasDefaultValue(0);
            builder.Property(p => p.BookTranslations).HasDefaultValue(0);
            builder.Property(p => p.ParticipationEditorialCommittees).HasDefaultValue(0);
            builder.Property(p => p.FullComposerSoloOrchestraAllTracks).HasDefaultValue(0);
            builder.Property(p => p.FullComposerSoloOrchestraCompilation).HasDefaultValue(0);
            builder.Property(p => p.ChamberOrchestraInterpretation).HasDefaultValue(0);
            builder.Property(p => p.IndividualAndCollectiveArtPerformances).HasDefaultValue(0);
            builder.Property(p => p.ScientificCulturalArtisticCollectionsCuratorship).HasDefaultValue(0);
            builder.Property(p => p.PatentLetter).HasDefaultValue(0);
            builder.Property(p => p.PatentDeposit).HasDefaultValue(0);
            builder.Property(p => p.SoftwareRegistration).HasDefaultValue(0);
            builder.Property(p => p.StudentId).IsRequired();
            builder.Property(p => p.ProgramTypeId).IsRequired();
            builder.Property(p => p.ProfessorId).IsRequired();
            builder.Property(p => p.SubAreaId).IsRequired();
            builder.Property(p => p.NoticeId).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.StatusDescription).IsRequired();
            builder.Property(p => p.AppealObservation);
            builder.Property(p => p.SubmissionDate);
            builder.Property(p => p.ResubmissionDate);
            builder.Property(p => p.CancellationDate);
            builder.Property(p => p.CancellationReason);
            builder.Property(p => p.DeletedAt);

            builder.HasQueryFilter(x => x.DeletedAt == null);
        }
    }
}