using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntitiesConfiguration
{
    public class NoticeConfiguration : IEntityTypeConfiguration<Notice>
    {
        public void Configure(EntityTypeBuilder<Notice> builder)
        {
            builder.ToTable("Notices");
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.StartDate).IsRequired();
            builder.Property(p => p.FinalDate).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(300);
            builder.Property(p => p.DocUrl).HasMaxLength(300);
            builder.Property(p => p.DeletedAt);

            builder.HasQueryFilter(x => x.DeletedAt == null);
        }
    }
}