using CopetSystem.API.Data;
using CopetSystem.API.GraphQL;
using Microsoft.EntityFrameworkCore;

namespace CopetSystem.API
{
  public class Startup
  {
    private readonly IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services){
      services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

      services
        .AddGraphQLServer()
        .AddQueryType<Query>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      // app.UseWebSockets();

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
          endpoints.MapGraphQL();
      });

      // app.UseGraphQLVoyager(new GraphQLVoyagerOptions()
      // {
      //     GraphQLEndPoint = "/graphql",
      //     Path = "/graphql-voyager"
      // });
    }
  }
}