using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntitiesConfiguration
{
    public class AreaConfiguration : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.ToTable("Areas");
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Code).HasMaxLength(100).IsRequired();
            builder.Property(p => p.MainAreaId).IsRequired();

            builder.HasOne(a => a.MainArea)
                .WithMany()
                .HasForeignKey(a => a.MainAreaId);
        }
    }
}