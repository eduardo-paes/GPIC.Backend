using AspNetCoreRateLimit;
using Infrastructure.IoC;
using WebAPI.Middlewares;

namespace WebAPI
{
    /// <summary>
    /// Classe de iniciação da WebAPI.
    /// </summary>
    public class Startup
    {
        private const string CORS_POLICY_NAME = "_allowSpecificOrigins";
        private IConfiguration? _configuration;

        /// <summary>
        /// Realiza a configuração dos serviços de injeção de dependência.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Adição dos Controllers
            services.AddControllers();

            // Realiza comunicação com os demais Projetos.
            services.AddInfrastructure(ref _configuration);
            services.AddPersistence();
            services.AddExternalServices();
            services.AddApplication();

            // Configuração do Swagger
            services.AddInfrastructureSwagger();

            // Configuração do JWT
            services.AddInfrastructureJWT();

            // Permite que rotas sejam acessíveis em lowercase
            services.AddRouting(options => options.LowercaseUrls = true);

            #region CORS
            // Definição de política de CORS
            services.AddCors(options =>
            {
                // Permite qualquer origem, cabeçalho e método
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });

                // // Busca os valores de ALLOW_ORIGINS do arquivo .env
                // var allowedOrigins = Environment.GetEnvironmentVariable("ALLOW_ORIGINS")
                //     ?? throw new Exception("ALLOW_ORIGINS não definido nas variáveis de ambiente.");

                // // Permite apenas as origens definidas no arquivo .env
                // options.AddPolicy(name: CORS_POLICY_NAME,
                //       policy =>
                //       {
                //           // Busca os valores de ALLOW_ORIGINS do arquivo .env
                //           policy.WithOrigins(
                //             origins: allowedOrigins.Split(',')!)
                //               .AllowAnyHeader()
                //               .AllowAnyMethod();
                //       });
            });
            #endregion CORS

            #region Rate Limit
            // Definido através do API Gateway
            // services.AddMemoryCache();
            // services.AddInMemoryRateLimiting();
            // services.Configure<ClientRateLimitOptions>(_configuration.GetSection("IpRateLimiting"));
            // services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();
            // services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            // services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            // services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            #endregion Rate Limit
        }

        /// <summary>
        /// Adiciona as configurações de segurança, documentação e roteamento.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable Swagger middleware for API documentation
            app.UseSwagger();

            if (env.IsDevelopment())
            {
                // Show detailed error page in development mode
                app.UseDeveloperExceptionPage();

                // Enable Swagger UI in development mode
                app.UseSwaggerUI();

                // Show development mode message
                Console.WriteLine("Swagger is up.");
            }

            // UseExceptionHandler for non-development environments
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            // Enable HTTP Strict Transport Security (HSTS) headers for secure communication
            app.UseHsts();

            // Redirect HTTP requests to HTTPS for secure communication
            app.UseHttpsRedirection();

            // Enable CORS
            app.UseCors();
            // app.UseCors(CORS_POLICY_NAME);

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