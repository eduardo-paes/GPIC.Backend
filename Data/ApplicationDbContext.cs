using DotnetGraphql.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetGraphQl.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; }
  }
}