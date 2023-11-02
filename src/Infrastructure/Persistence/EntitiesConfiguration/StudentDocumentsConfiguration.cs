using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntitiesConfiguration
{
    public class StudentDocumentsConfiguration : IEntityTypeConfiguration<StudentDocuments>
    {
        public void Configure(EntityTypeBuilder<StudentDocuments> builder)
        {
            _ = builder.ToTable("StudentDocuments");
            _ = builder.HasKey(t => t.Id);
            _ = builder.Property(p => p.Id).ValueGeneratedOnAdd();

            _ = builder.Property(p => p.IdentityDocument).IsRequired();
            _ = builder.Property(p => p.CPF).IsRequired();
            _ = builder.Property(p => p.Photo3x4).IsRequired();
            _ = builder.Property(p => p.SchoolHistory).IsRequired();
            _ = builder.Property(p => p.ScholarCommitmentAgreement).IsRequired();
            _ = builder.Property(p => p.ParentalAuthorization);
            _ = builder.Property(p => p.AgencyNumber).IsRequired();
            _ = builder.Property(p => p.AccountNumber).IsRequired();
            _ = builder.Property(p => p.AccountOpeningProof).IsRequired();
            _ = builder.Property(p => p.ProjectId).IsRequired();

            _ = builder.HasOne(a => a.Project)
                .WithOne()
                .HasForeignKey<StudentDocuments>(a => a.ProjectId);

            _ = builder.HasQueryFilter(x => x.DeletedAt == null);
        }
    }
}