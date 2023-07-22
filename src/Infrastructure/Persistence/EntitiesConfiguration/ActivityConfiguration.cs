using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntitiesConfiguration
{
    public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable("Activities");

            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Name).HasMaxLength(300).IsRequired();
            builder.Property(p => p.Points).IsRequired();
            builder.Property(p => p.Limits);
            builder.Property(p => p.ActivityTypeId).IsRequired();
            builder.Property(p => p.DeletedAt);

            builder.HasOne(a => a.ActivityType)
                .WithMany()
                .HasForeignKey(a => a.ActivityTypeId);

            builder.HasQueryFilter(x => x.DeletedAt == null);
        }
    }
}