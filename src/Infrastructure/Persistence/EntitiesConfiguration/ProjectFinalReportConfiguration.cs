using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntitiesConfiguration
{
    public class ProjectFinalReportConfiguration : IEntityTypeConfiguration<ProjectFinalReport>
    {
        public void Configure(EntityTypeBuilder<ProjectFinalReport> builder)
        {
            builder.ToTable("ProjectFinalReports");
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.ReportUrl)
                .IsRequired();
            builder.Property(p => p.SendDate)
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