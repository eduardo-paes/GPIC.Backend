using System;
using CopetSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CopetSystem.Infra.Data.EntitiesConfiguration
{
	public class AreaConfiguration : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Code).HasMaxLength(100).IsRequired();
            builder.Property(p => p.MainAreaId).IsRequired();

            builder.HasData(
                new Area(1, "Area 1", "ABC-123", 1),
                new Area(2, "Area 2", "DEF-456", 2),
                new Area(3, "Area 3", "GHI-789", 3)
            );
        }
    }

