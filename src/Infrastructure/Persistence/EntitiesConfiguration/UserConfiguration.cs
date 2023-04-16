using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Name).HasMaxLength(300).IsRequired();
            builder.Property(p => p.Email).HasMaxLength(300).IsRequired();
            builder.Property(p => p.Password).HasMaxLength(300).IsRequired();
            builder.Property(p => p.CPF).HasMaxLength(15).IsRequired();
            builder.Property(p => p.Role).HasMaxLength(30).IsRequired();
            builder.Property(p => p.ValidationCode).HasMaxLength(6);
            builder.Property(p => p.IsConfirmed).IsRequired();
            builder.Property(p => p.DeletedAt);
        }
    }
}