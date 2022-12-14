namespace DotnetGraphql
{
  public class Startup
  {
    private readonly IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public void ConfigureService(IServiceCollection services)
    {

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseWebSockets();

      app.UseRouting();

      // app.UseEndpoints(endpoints =>
      // {
      //     endpoints.MapGraphQL();
      // });

      // app.UseGraphQLVoyager(new GraphQLVoyagerOptions()
      // {
      //     GraphQLEndPoint = "/graphql",
      //     Path = "/graphql-voyager"
      // });
    }
  }
}