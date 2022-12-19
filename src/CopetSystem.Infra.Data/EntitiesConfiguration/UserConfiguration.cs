using System;
using CopetSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CopetSystem.Infra.Data.EntitiesConfiguration
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Name).HasMaxLength(300).IsRequired();
            builder.Property(p => p.Email).HasMaxLength(300).IsRequired();
            builder.Property(p => p.Password).HasMaxLength(300).IsRequired();
            builder.Property(p => p.CPF).HasMaxLength(15).IsRequired();
            builder.Property(p => p.Role).HasMaxLength(30).IsRequired();

            builder.HasData(
                new User("Eduardo Paes", "eduardo.paes@email.com", "123456", "15162901784", "ADMIN")
            );
        }
    }
}

