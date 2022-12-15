using DotnetGraphql.Models;
using DotnetGraphQl.Data;

namespace DotnetGraphQl.GraphQL
{
  public class Query
  {
    public IQueryable<User> GetUsers([Service] ApplicationDbContext context)
    {
      return context.Users;
    }
  }
}