using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Domain.Contracts.Auth;
using Domain.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.Services;
public class TokenAuthenticationService : ITokenAuthenticationService
{
    #region Global Scope
    private readonly IDotEnvSecrets _dotEnvSecrets;
    public TokenAuthenticationService(IDotEnvSecrets dotEnvSecrets) => _dotEnvSecrets = dotEnvSecrets;
    #endregion

    public UserLoginOutput GenerateToken(Guid? id, string? userName, string? role)
    {
        // Verifica se o id é nulo
        if (id == null)
            throw new Exception("Id do usuário não informado.");

        // Verifica se o nome do usuário é nulo
        if (string.IsNullOrEmpty(userName))
            throw new Exception("Nome do usuário não informado.");

        // Verifica se o perfil do usuário é nulo
        if (string.IsNullOrEmpty(role))
            throw new Exception("Perfil do usuário não informado.");

        // Declaração do usuário
        var claims = new[]
        {
            new Claim(ClaimTypes.Sid, id.Value.ToString()),
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Gerar chave privada para assinar o token
        var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_dotEnvSecrets.GetJwtSecret()
            ?? throw new Exception("Chave secreta não informada.")));

        // Gerar a assinatura digital
        var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

        // Tempo de expiração do token
        var expireIn = int.Parse(_dotEnvSecrets.GetJwtExpirationTime() ?? "10");

        // Definir o tempo de expiração
        var expiration = DateTime.UtcNow.AddMinutes(expireIn);

        // Gerar o Token
        var token = new JwtSecurityToken(
            issuer: _dotEnvSecrets.GetJwtIssuer() ?? throw new Exception("Emissor do token não informado."),
            audience: _dotEnvSecrets.GetJwtAudience() ?? throw new Exception("Público do token não informado."),
            claims: claims,
            expires: expiration,
            signingCredentials: credentials);

        return new UserLoginOutput()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
    }
}
