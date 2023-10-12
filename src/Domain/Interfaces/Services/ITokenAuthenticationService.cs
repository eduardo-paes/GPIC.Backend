using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface ITokenAuthenticationService
    {
        /// <summary>
        /// Gera um token de autenticação com base no id e role do usuário.
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <param name="actorId">Id do professor ou do aluno</param>
        /// <param name="userName">Nome do usuário</param>
        /// <param name="role">Perfil do usuário</param>
        /// <returns>Token de autenticação.</returns>
        string GenerateToken(Guid? id, Guid? actorId, string? userName, string? role);

        /// <summary>
        /// Retorna as claims do usuário autenticado.
        /// </summary>
        Dictionary<Guid, User> GetUserAuthenticatedClaims();
    }
}