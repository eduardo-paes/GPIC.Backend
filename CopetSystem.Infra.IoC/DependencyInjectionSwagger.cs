using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CopetSystem.Infra.IoC
{
	public static class DependencyInjectionSwagger
	{
        public static IServiceCollection AddInfrastructureSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                // Adiciona documentação com Swagger
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CopetSystem.API",
                    Version = "v1",
                    Description = "API Rest criada em .NET 6 para controle de projetos de iniciação científica do CEFET."
                });

                // Adiciona comentários dos métodos nas rotas do Swagger
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "CopetSystem.API.xml"));

                //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                //{
                //    Name = "Authorization",
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer",
                //    BearerFormat = "JWT",
                //    In = ParameterLocation.Header,
                //    Description = "JWT Authorization header using the Bearer scheme."
                //});

                //c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference
                //            {
                //                Type = ReferenceType.SecurityScheme,
                //                Id = "Bearer"
                //            }
                //        },
                //        System.Array.Empty<string>()
                //    }
                //});
            });
            services.AddSingleton<SwaggerGenerator>();
            return services;
        }
    }
}

