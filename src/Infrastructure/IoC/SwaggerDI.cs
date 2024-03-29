﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Infrastructure.IoC
{
    public static class SwaggerDI
    {
        public static IServiceCollection AddInfrastructureSwagger(this IServiceCollection services)
        {
            _ = services.AddEndpointsApiExplorer();
            _ = services.AddSwaggerGen(c =>
            {
                // Adiciona documentação com Swagger
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Infrastructure.WebAPI",
                    Description = "API Rest criada em .NET 7.0 para controle de projetos de iniciação científica do CEFET."
                });

                // Adiciona comentários dos métodos nas rotas do Swagger
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Infrastructure.WebAPI.xml"));

                // Adiciona o JWT como esquema de segurança
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                // Adiciona o JWT como esquema de segurança global para todas as rotas
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                   {
                       new OpenApiSecurityScheme
                       {
                           Reference = new OpenApiReference
                           {
                               Type = ReferenceType.SecurityScheme,
                               Id = "Bearer"
                           }
                       },
                       Array.Empty<string>()
                   }
                });
            });
            _ = services.AddSingleton<SwaggerGenerator>();
            return services;
        }
    }
}