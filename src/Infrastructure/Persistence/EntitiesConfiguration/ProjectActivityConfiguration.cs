using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntitiesConfiguration
{
    public class ProjectActivityConfiguration : IEntityTypeConfiguration<ProjectActivity>
    {
        public void Configure(EntityTypeBuilder<ProjectActivity> builder)
        {
            builder.ToTable("ProjectActivities");

            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.ActivityId).IsRequired();
            builder.Property(p => p.ProjectId).IsRequired();
            builder.Property(p => p.InformedActivities).IsRequired();
            builder.Property(p => p.FoundActivities);
            builder.Property(p => p.DeletedAt);

            builder.HasOne(a => a.Project)
                .WithMany()
                .HasForeignKey(a => a.ProjectId);
            builder.HasOne(a => a.Activity)
                .WithMany()
                .HasForeignKey(a => a.ActivityId);

            builder.HasQueryFilter(x => x.DeletedAt == null);
        }
    }
}