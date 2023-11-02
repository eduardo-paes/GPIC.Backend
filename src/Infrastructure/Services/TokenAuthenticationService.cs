using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Services
{
    public class TokenAuthenticationService : ITokenAuthenticationService
    {
        #region Global Scope
        private readonly IDotEnvSecrets _dotEnvSecrets;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenAuthenticationService(IDotEnvSecrets dotEnvSecrets, IHttpContextAccessor httpContextAccessor)
        {
            _dotEnvSecrets = dotEnvSecrets;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion Global Scope

        /// <summary>
        /// Gera o token de autenticação.
        /// </summary>
        /// <param name="id">Id do usuário.</param>
        /// <param name="actorId">Id do professor ou do aluno.</param>
        /// <param name="userName">Nome do usuário.</param>
        /// <param name="role">Perfil do usuário.</param>
        /// <returns>Token de autenticação.</returns>
        public string GenerateToken(Guid? id, Guid? actorId, string? userName, string? role)
        {
            // Verifica se o id é nulo
            if (id == null)
            {
                throw new Exception("Id do usuário não informado.");
            }

            // Verifica se o nome do usuário é nulo
            if (string.IsNullOrEmpty(userName))
            {
                throw new Exception("Nome do usuário não informado.");
            }

            // Verifica se o perfil do usuário é nulo
            if (string.IsNullOrEmpty(role))
            {
                throw new Exception("Perfil do usuário não informado.");
            }

            // Verifica se o id do professor ou estudante é nulo
            if (actorId == null)
            {
                throw new Exception($"Id do {role} inválido.");
            }

            // Declaração do usuário
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Sid, id.Value.ToString()),
                new Claim(ClaimTypes.Actor, actorId.Value.ToString()),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Gerar chave privada para assinar o token
            SymmetricSecurityKey privateKey = new(Encoding.UTF8.GetBytes(_dotEnvSecrets.GetJwtSecret()
                ?? throw new Exception("Chave secreta não informada.")));

            // Gerar a assinatura digital
            SigningCredentials credentials = new(privateKey, SecurityAlgorithms.HmacSha256);

            // Tempo de expiração do token
            int expireIn = int.Parse(_dotEnvSecrets.GetJwtExpirationTime() ?? "10");

            // Definir o tempo de expiração
            DateTime expiration = DateTime.UtcNow.AddMinutes(expireIn);

            // Gerar o Token
            JwtSecurityToken token = new(
                issuer: _dotEnvSecrets.GetJwtIssuer() ?? throw new Exception("Emissor do token não informado."),
                audience: _dotEnvSecrets.GetJwtAudience() ?? throw new Exception("Público do token não informado."),
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Retorna as claims do usuário autenticado.
        /// </summary>
        /// <returns>Id, Name e Role.</returns>
        public Dictionary<Guid, User> GetUserAuthenticatedClaims()
        {
            // Get the current HttpContext to retrieve the claims principal
            HttpContext? httpContext = _httpContextAccessor.HttpContext;

            // Check if the user is authenticated
            if (httpContext?.User.Identity == null || httpContext?.User.Identity.IsAuthenticated != true)
            {
                throw new Exception("Usuário não autenticado.");
            }

            // Get the claims principal
            ClaimsIdentity claimsIdentity = httpContext.User.Identity as ClaimsIdentity
                ?? throw new Exception("Usuário não autenticado.");

            // Get the user's ID
            string? id = claimsIdentity.FindFirst(ClaimTypes.Sid)?.Value;
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("Id do usuário não informado.");
            }

            string? actorId = claimsIdentity.FindFirst(ClaimTypes.Actor)?.Value;
            if (string.IsNullOrEmpty(actorId))
            {
                throw new Exception($"Id do {ClaimTypes.Role} não informado.");
            }

            // Cria o output com o id do professor ou estudade e o usuário
            var user = new User(Guid.Parse(id), claimsIdentity.FindFirst(ClaimTypes.Name)?.Value, claimsIdentity.FindFirst(ClaimTypes.Role)?.Value);
            return new Dictionary<Guid, User>
            {
                { Guid.Parse(actorId), user }
            };
        }
    }
}
