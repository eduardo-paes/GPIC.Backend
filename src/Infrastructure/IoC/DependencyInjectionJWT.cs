using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.IoC;
public static class DependencyInjectionJWT
{
    public static IServiceCollection AddInfrastructureJWT(this IServiceCollection services)
    {
        // Define valores das propriedades de configuração
        IConfiguration configuration = SettingsConfiguration.GetConfiguration();

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
            // Pega a chave secreta
            string? secretKey = configuration.GetSection("Jwt:SecretKey").Value;
            if (string.IsNullOrEmpty(secretKey))
                throw new ArgumentNullException("Jwt:SecretKey");

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                /// Valores válidos
                ValidIssuer = configuration.GetSection("Jwt:Issuer").Value,
                ValidAudience = configuration.GetSection("Jwt:Audience").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                /// Se não fizer isso ele vai inserir + 5min em cima
                /// do que foi definido na geração do Token.
                ClockSkew = TimeSpan.Zero
            };
        });
        return services;
    }
}