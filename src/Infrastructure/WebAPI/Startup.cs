using Infrastructure.IoC;

namespace Infrastructure.WebAPI
{
    /// <summary>
    /// Classe de iniciação da WebAPI.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Propriedade de configuração.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Construtor da classe.
        /// </summary>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Realiza a configuração dos serviços de injeção de dependência.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            // Adição dos Controllers
            services.AddControllers();

            // Realiza comunicação com os demais Projetos.
            services.AddInfrastructure(Configuration);
            services.AddAdapters();
            services.AddDomain();

            // Configuração do Swagger
            services.AddInfrastructureSwagger();

            // Permite que rotas sejam acessíveis em lowercase
            services.AddRouting(options => options.LowercaseUrls = true);
        }

        /// <summary>
        /// Adiciona as configurações de segurança, documentação e roteamento.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseHsts();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
