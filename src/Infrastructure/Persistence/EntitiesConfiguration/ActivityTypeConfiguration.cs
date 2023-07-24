using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntitiesConfiguration
{
    public class ActivityTypeConfiguration : IEntityTypeConfiguration<ActivityType>
    {
        public void Configure(EntityTypeBuilder<ActivityType> builder)
        {
            builder.ToTable("ActivityTypes");

            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Name).HasMaxLength(300).IsRequired();
            builder.Property(p => p.Unity).HasMaxLength(300).IsRequired();
            builder.Property(p => p.NoticeId).IsRequired();
            builder.Property(p => p.DeletedAt);

            builder.HasOne(a => a.Notice)
                .WithMany()
                .HasForeignKey(a => a.NoticeId);

            builder.HasQueryFilter(x => x.DeletedAt == null);
        }
    }
}