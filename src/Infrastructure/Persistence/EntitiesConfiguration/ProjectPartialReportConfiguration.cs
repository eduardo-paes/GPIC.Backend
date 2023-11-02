using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntitiesConfiguration
{
    public class ProjectPartialReportConfiguration : IEntityTypeConfiguration<ProjectPartialReport>
    {
        public void Configure(EntityTypeBuilder<ProjectPartialReport> builder)
        {
            builder.ToTable("ProjectPartialReports");
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.AdditionalInfo);
            builder.Property(p => p.CurrentDevelopmentStage)
                .IsRequired();
            builder.Property(p => p.ScholarPerformance)
                .IsRequired();
            builder.Property(p => p.ProjectId)
                .IsRequired();
            builder.Property(p => p.UserId)
                .IsRequired();
            builder.Property(p => p.DeletedAt);

            builder.HasOne(a => a.Project)
                .WithMany()
                .HasForeignKey(a => a.ProjectId);
            builder.HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId);

            builder.HasQueryFilter(x => x.DeletedAt == null);
        }
    }
}