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
            builder.Property(p => p.RegistrationStartDate).IsRequired();
            builder.Property(p => p.RegistrationEndDate).IsRequired();
            builder.Property(p => p.EvaluationStartDate).IsRequired();
            builder.Property(p => p.EvaluationEndDate).IsRequired();
            builder.Property(p => p.AppealStartDate).IsRequired();
            builder.Property(p => p.AppealEndDate).IsRequired();
            builder.Property(p => p.SendingDocsStartDate).IsRequired();
            builder.Property(p => p.SendingDocsEndDate).IsRequired();
            builder.Property(p => p.PartialReportDeadline).IsRequired();
            builder.Property(p => p.FinalReportDeadline).IsRequired();
            builder.Property(p => p.SuspensionYears).IsRequired();
            builder.Property(p => p.DocUrl).HasMaxLength(300);
            builder.Property(p => p.CreatedAt).HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(p => p.DeletedAt);

            builder.HasQueryFilter(x => x.DeletedAt == null);
        }
    }
}