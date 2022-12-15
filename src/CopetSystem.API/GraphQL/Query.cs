using CopetSystem.API.Models;
using CopetSystem.API.Data;

namespace CopetSystem.API.GraphQL
{
  public class Query
  {
    public IQueryable<User> GetUsers([Service] ApplicationDbContext context)
    {
      return context.Users;
    }
  }
}