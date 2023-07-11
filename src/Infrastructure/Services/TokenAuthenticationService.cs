using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Domain.Contracts.Auth;
using Domain.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;
public class TokenAuthenticationService : ITokenAuthenticationService
{
    #region Global Scope
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenAuthenticationService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }
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
        var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Secret").Value
            ?? throw new Exception("Chave secreta não informada.")));

        // Gerar a assinatura digital
        var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

        // Tempo de expiração do token
        var expireIn = int.Parse(_configuration.GetSection("Jwt:ExpireIn").Value ?? "10");

        // Definir o tempo de expiração
        var expiration = DateTime.UtcNow.AddMinutes(expireIn);

        // Gerar o Token
        var token = new JwtSecurityToken(
            issuer: _configuration.GetSection("Jwt:Issuer").Value ?? throw new Exception("Emissor do token não informado."),
            audience: _configuration.GetSection("Jwt:Audience").Value ?? throw new Exception("Público do token não informado."),
            claims: claims,
            expires: expiration,
            signingCredentials: credentials);

        return new UserLoginOutput()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
    }

    /// <summary>
    /// Retorna as claims do usuário autenticado.
    /// </summary>
    /// <returns>Id, Name e Role.</returns>
    public UserClaimsOutput GetUserAuthenticatedClaims()
    {
        // Get the current HttpContext to retrieve the claims principal
        var httpContext = _httpContextAccessor.HttpContext;

        // Check if the user is authenticated
        if (httpContext?.User.Identity == null || httpContext?.User.Identity.IsAuthenticated != true)
            throw new Exception("User is not authenticated.");

        // Get the claims principal
        var claimsIdentity = httpContext.User.Identity as ClaimsIdentity
            ?? throw new Exception("User is not authenticated.");

        // Get the user's ID
        var id = claimsIdentity.FindFirst(ClaimTypes.Sid)?.Value;
        if (string.IsNullOrEmpty(id))
            throw new Exception("User ID not provided.");

        // Return the user's claims
        return new UserClaimsOutput()
        {
            Id = Guid.Parse(id),
            Name = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value,
            Role = claimsIdentity.FindFirst(ClaimTypes.Role)?.Value
        };
    }
}
