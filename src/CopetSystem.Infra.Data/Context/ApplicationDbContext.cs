using CopetSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CopetSystem.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<MainArea> MainAreas { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<SubArea> SubAreas { get; set; }

        public DbSet<ProgramType> ProgramTypes { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<StudentAssistanceScholarship> AssistanceScholarships { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}

