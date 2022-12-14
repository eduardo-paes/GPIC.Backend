using DotnetGraphql.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetGraphQl.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
  }
}