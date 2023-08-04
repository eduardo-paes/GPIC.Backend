using AspNetCoreRateLimit;
using IoC;
using WebAPI.Middlewares;

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
            services.AddControllers();

            // Realiza comunicação com os demais Projetos.
            services.AddInfrastructure();
            services.AddAdapters();
            services.AddDomain();

            // Configuração do Swagger
            services.AddInfrastructureSwagger();

            // Configuração do JWT
            services.AddInfrastructureJWT();

            // Permite que rotas sejam acessíveis em lowercase
            services.AddRouting(options => options.LowercaseUrls = true);
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
                app.UseDeveloperExceptionPage();

                // Enable Swagger middleware for API documentation in development mode
                app.UseSwagger();
                app.UseSwaggerUI();

                // Show development mode message
                Console.WriteLine("Development mode");
            }

            // UseExceptionHandler for non-development environments
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            // Enable HTTP Strict Transport Security (HSTS) headers for secure communication
            app.UseHsts();

            // Redirect HTTP requests to HTTPS for secure communication
            app.UseHttpsRedirection();

            // Enable CORS
            app.UseCors();

            // Enable routing for incoming requests
            app.UseRouting();

            // Enable authentication for the API
            app.UseAuthentication();

            // Enable authorization for the API
            app.UseAuthorization();

            // Apply rate limiting middleware to control the number of requests allowed  
            app.UseClientRateLimiting();
            app.UseIpRateLimiting();

            // Configure API endpoints
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}