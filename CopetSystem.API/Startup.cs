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
            // Realiza comunicação com os demais Projetos.
            services.AddInfrastructure(Configuration);

            // Configuração do Swagger
            services.AddInfrastructureSwagger();

            // Adição dos Controllers
            services.AddControllers();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CopetSystem.API v1"));
            }

            app.UseHttpsRedirection();
            // Trata as respostas dos Endpoints
            app.UseStatusCodePages();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}