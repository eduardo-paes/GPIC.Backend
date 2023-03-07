using CopetSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CopetSystem.Infra.Data.EntitiesConfiguration
{
    public class SubAreaConfiguration : IEntityTypeConfiguration<SubArea>
    {
        public void Configure(EntityTypeBuilder<SubArea> builder)
        {
            builder.ToTable("SubAreas");
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Code).HasMaxLength(100).IsRequired();
            builder.Property(p => p.AreaId).IsRequired();

            builder.HasOne(a => a.Area)
                .WithMany()
                .HasForeignKey(a => a.AreaId);
        }
    }
}