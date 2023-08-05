using Application.Ports.Auth;

namespace Application.Interfaces.Services
{
    public interface ITokenAuthenticationService
    {
        /// <summary>
        /// Gera um token de autenticação com base no id e role do usuário.
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <param name="userName">Nome do usuário</param>
        /// <param name="role">Perfil do usuário</param>
        /// <returns>Token de autenticação.</returns>
        UserLoginOutput GenerateToken(Guid? id, string? userName, string? role);

        /// <summary>
        /// Retorna as claims do usuário autenticado.
        /// </summary>
        UserClaimsOutput GetUserAuthenticatedClaims();
    }
}