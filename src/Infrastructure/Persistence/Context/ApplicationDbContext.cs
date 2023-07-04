using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DbContext" /> class using the specified options.
        ///     The <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method will still be called to allow further
        ///     configuration of the options.
        /// </summary>
        /// <remarks>
        ///     See <see href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</see> and
        ///     <see href="https://aka.ms/efcore-docs-dbcontext-options">Using DbContextOptions</see> for more information and examples.
        /// </remarks>
        /// <param name="options">The options for this context.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Areas = Set<Area>();
            AssistanceScholarships = Set<StudentAssistanceScholarship>();
            Campuses = Set<Campus>();
            Courses = Set<Course>();
            MainAreas = Set<MainArea>();
            Notices = Set<Notice>();
            Professors = Set<Professor>();
            Projects = Set<Project>();
            ProgramTypes = Set<ProgramType>();
            StudentAssistanceScholarships = Set<StudentAssistanceScholarship>();
            Students = Set<Student>();
            SubAreas = Set<SubArea>();
            Users = Set<User>();
        }

        public DbSet<Area> Areas { get; set; }
        public DbSet<StudentAssistanceScholarship> AssistanceScholarships { get; set; }
        public DbSet<Campus> Campuses { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<MainArea> MainAreas { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProgramType> ProgramTypes { get; set; }
        public DbSet<StudentAssistanceScholarship> StudentAssistanceScholarships { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<SubArea> SubAreas { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("public");
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}