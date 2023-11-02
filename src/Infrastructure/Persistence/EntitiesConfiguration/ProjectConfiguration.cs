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
            builder.Property(p => p.StudentId);
            builder.Property(p => p.ProgramTypeId).IsRequired();
            builder.Property(p => p.ProfessorId).IsRequired();
            builder.Property(p => p.SubAreaId).IsRequired();
            builder.Property(p => p.NoticeId).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.StatusDescription).IsRequired();
            builder.Property(p => p.AppealObservation);
            builder.Property(p => p.SubmissionDate);
            builder.Property(p => p.AppealDate);
            builder.Property(p => p.CancellationDate);
            builder.Property(p => p.CancellationReason);
            builder.Property(p => p.CertificateUrl);
            builder.Property(p => p.DeletedAt);

            builder.HasOne(a => a.Student)
                .WithMany()
                .HasForeignKey(a => a.StudentId);
            builder.HasOne(a => a.ProgramType)
                .WithMany()
                .HasForeignKey(a => a.ProgramTypeId);
            builder.HasOne(a => a.Professor)
                .WithMany()
                .HasForeignKey(a => a.ProfessorId);
            builder.HasOne(a => a.SubArea)
                .WithMany()
                .HasForeignKey(a => a.SubAreaId);
            builder.HasOne(a => a.Notice)
                .WithMany()
                .HasForeignKey(a => a.NoticeId);

            builder.HasQueryFilter(x => x.DeletedAt == null);
        }
    }
}