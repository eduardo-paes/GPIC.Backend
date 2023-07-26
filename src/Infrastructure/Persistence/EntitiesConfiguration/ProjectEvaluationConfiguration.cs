using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntitiesConfiguration
{
    public class ProjectEvaluationConfiguration : IEntityTypeConfiguration<ProjectEvaluation>
    {
        public void Configure(EntityTypeBuilder<ProjectEvaluation> builder)
        {
            builder.ToTable("ProjectEvaluations");
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.IsProductivityFellow).IsRequired();
            builder.Property(p => p.SubmissionEvaluationStatus).IsRequired();
            builder.Property(p => p.SubmissionEvaluationDate).IsRequired();
            builder.Property(p => p.SubmissionEvaluationDescription).IsRequired();
            builder.Property(p => p.AppealEvaluationStatus);
            builder.Property(p => p.AppealEvaluationDate);
            builder.Property(p => p.AppealEvaluationDescription);
            builder.Property(p => p.DocumentsEvaluationDate);
            builder.Property(p => p.DocumentsEvaluationDescription);

            builder.Property(p => p.APIndex).HasDefaultValue(0);
            builder.Property(p => p.FinalScore).HasDefaultValue(0);
            builder.Property(p => p.Qualification).IsRequired();
            builder.Property(p => p.ProjectProposalObjectives).IsRequired();
            builder.Property(p => p.AcademicScientificProductionCoherence).IsRequired();
            builder.Property(p => p.ProposalMethodologyAdaptation).IsRequired();
            builder.Property(p => p.EffectiveContributionToResearch).IsRequired();

            builder.Property(p => p.ProjectId).IsRequired();
            builder.Property(p => p.SubmissionEvaluatorId).IsRequired();
            builder.Property(p => p.AppealEvaluatorId);
            builder.Property(p => p.DocumentsEvaluatorId);

            builder.HasOne(a => a.Project)
                .WithOne()
                .HasForeignKey<ProjectEvaluation>(a => a.ProjectId);
            builder.HasOne(a => a.SubmissionEvaluator)
                .WithMany()
                .HasForeignKey(a => a.SubmissionEvaluatorId);
            builder.HasOne(a => a.AppealEvaluator)
                .WithMany()
                .HasForeignKey(a => a.AppealEvaluatorId);
            builder.HasOne(a => a.DocumentsEvaluator)
                .WithMany()
                .HasForeignKey(a => a.DocumentsEvaluatorId);
        }
    }
}