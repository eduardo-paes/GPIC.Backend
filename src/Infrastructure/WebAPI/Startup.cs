using AspNetCoreRateLimit;
using Infrastructure.IoC;
using Infrastructure.WebAPI.Middleware;

namespace WebAPI
{
    /// <summary>
    /// Classe de iniciação da WebAPI.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Realiza a configuração dos serviços de injeção de dependência.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Adição dos Controllers
            _ = services.AddControllers();

            // Realiza comunicação com os demais Projetos.
            _ = services.AddInfrastructure();
            _ = services.AddAdapters();
            _ = services.AddDomain();

            // Configuração do Swagger
            _ = services.AddInfrastructureSwagger();

            // Configuração do JWT
            _ = services.AddInfrastructureJWT();

            // Permite que rotas sejam acessíveis em lowercase
            _ = services.AddRouting(options => options.LowercaseUrls = true);

            // Configuração do CORS
            _ = services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        _ = policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });
        }

        /// <summary>
        /// Adiciona as configurações de segurança, documentação e roteamento.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Show detailed error page in development mode
                _ = app.UseDeveloperExceptionPage();

                // Enable Swagger middleware for API documentation in development mode
                _ = app.UseSwagger();
                _ = app.UseSwaggerUI();

                // Show development mode message
                Console.WriteLine("Development mode");
            }

            // UseExceptionHandler for non-development environments
            _ = app.UseMiddleware<ExceptionHandlingMiddleware>();

            // Enable HTTP Strict Transport Security (HSTS) headers for secure communication
            _ = app.UseHsts();

            // Redirect HTTP requests to HTTPS for secure communication
            _ = app.UseHttpsRedirection();

            // Enable routing for incoming requests
            _ = app.UseRouting();

            // Enable authentication for the API
            _ = app.UseAuthentication();

            // Enable authorization for the API
            _ = app.UseAuthorization();

            // Apply rate limiting middleware to control the number of requests allowed  
            _ = app.UseClientRateLimiting();
            _ = app.UseIpRateLimiting();

            // Configure API endpoints
            _ = app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}