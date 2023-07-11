using Infrastructure.IoC.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.IoC;
public static class DependencyInjectionJWT
{
    public static IServiceCollection AddInfrastructureJWT(this IServiceCollection services)
    {
        // Carrega informações de ambiente (appsettings.json)
        var configuration = SettingsConfiguration.GetConfiguration();
        var validIssuer = configuration.GetSection("Jwt:Issuer").Value;
        var validAudience = configuration.GetSection("Jwt:Audience").Value;
        var issuerSigningKey = configuration.GetSection("Jwt:Secret").Value;

        /// Informar o tipo de autenticação;
        /// Definir o modelo de desafio de autenticação.
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })

        /// Habilita a autenticação JWT usando o esquema e desafio definidos;
        /// Validar o token.
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                /// Valores válidos
                ValidIssuer = validIssuer,
                ValidAudience = validAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey!)),
                /// Se não fizer isso ele vai inserir + 5min em cima
                /// do que foi definido na geração do Token.
                ClockSkew = TimeSpan.Zero
            };
        });

        /// Define as políticas de autorização
        services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("ADMIN"));
            options.AddPolicy("RequireProfessorRole", policy => policy.RequireRole("PROFESSOR"));
            options.AddPolicy("RequireStudentRole", policy => policy.RequireRole("STUDENT"));
        });
        return services;
    }
}