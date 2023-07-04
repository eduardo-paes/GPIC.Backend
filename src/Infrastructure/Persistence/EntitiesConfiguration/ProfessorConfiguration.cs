using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntitiesConfiguration
{
    public class ProfessorConfiguration : IEntityTypeConfiguration<Professor>
    {
        public void Configure(EntityTypeBuilder<Professor> builder)
        {
            builder.ToTable("Professors");
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            builder.Property(p => p.IdentifyLattes)
                .IsRequired();
            builder.Property(p => p.SIAPEEnrollment)
                .HasMaxLength(7)
                .IsRequired();
            builder.Property(p => p.UserId)
                .IsRequired();
            builder.Property(p => p.DeletedAt);

            builder.HasOne(a => a.User)
                .WithOne()
                .HasForeignKey<Professor>(a => a.UserId);

            builder.HasQueryFilter(x => x.DeletedAt == null);
        }
    }
}