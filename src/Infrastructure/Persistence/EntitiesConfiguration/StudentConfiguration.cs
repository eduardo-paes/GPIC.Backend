using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntitiesConfiguration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            builder.Property(p => p.BirthDate)
                .IsRequired();
            builder.Property(p => p.RG)
                .IsRequired();
            builder.Property(p => p.IssuingAgency)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(p => p.DispatchDate)
                .IsRequired();
            builder.Property(p => p.Gender)
                .IsRequired();
            builder.Property(p => p.Race)
                .IsRequired();
            builder.Property(p => p.HomeAddress)
                .IsRequired()
                .HasMaxLength(300);
            builder.Property(p => p.City)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(p => p.UF)
                .IsRequired()
                .HasMaxLength(2)
                .IsFixedLength();
            builder.Property(p => p.CEP)
                .IsRequired();
            builder.Property(p => p.PhoneDDD);
            builder.Property(p => p.Phone);
            builder.Property(p => p.CellPhoneDDD);
            builder.Property(p => p.CellPhone);
            builder.Property(p => p.CampusId)
                .IsRequired();
            builder.Property(p => p.CourseId)
                .IsRequired();
            builder.Property(p => p.StartYear)
                .IsRequired();
            builder.Property(p => p.StudentAssistanceProgram)
                .IsRequired();
            builder.Property(p => p.UserId)
                .IsRequired();
            builder.Property(p => p.DeletedAt);

            builder.HasOne(a => a.User).WithOne()
            .HasForeignKey<Student>(a => a.UserId);
            builder.HasOne(a => a.Campus).WithMany().HasForeignKey(a => a.CampusId);
            builder.HasOne(a => a.Course).WithMany().HasForeignKey(a => a.CourseId);
        }
    }
}