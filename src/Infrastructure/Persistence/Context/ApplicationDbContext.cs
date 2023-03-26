using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Seeds;

namespace Infrastructure.Persistence.Context
{
    public class AdaptersDbContext : DbContext
    {
        public AdaptersDbContext(DbContextOptions<AdaptersDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<MainArea> MainAreas { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<SubArea> SubAreas { get; set; }

        //public DbSet<ProgramType> ProgramTypes { get; set; }
        //public DbSet<Project> Projects { get; set; }
        //public DbSet<StudentAssistanceScholarship> AssistanceScholarships { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("public");
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(AdaptersDbContext).Assembly);
        }
    }
}