using CopetSystem.API.Queries;
using CopetSystem.Infra.Data.Repositories;
using CopetSystem.Infra.IoC;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CopetSystem.API
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(Configuration);
            services
              .AddScoped<UserQuery>()
              .AddGraphQLServer()
              .AddQueryType<UserQuery>()
              .AddFiltering()
              .AddSorting();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSockets();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL("/graphql");
                endpoints.MapGraphQLVoyager("/graphql-voyager");
            });
        }
    }
}