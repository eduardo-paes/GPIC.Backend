using System;
using CopetSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CopetSystem.Infra.Data.EntitiesConfiguration
{
    public class MainAreaConfiguration : IEntityTypeConfiguration<MainArea>
    {
        public void Configure(EntityTypeBuilder<MainArea> builder)
        {
            builder.ToTable("MainAreas");
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Code).HasMaxLength(100).IsRequired();
        }
    }
}

