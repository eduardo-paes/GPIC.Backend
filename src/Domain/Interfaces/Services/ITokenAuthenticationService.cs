namespace Domain.Interfaces.Services;
public interface ITokenAuthenticationService
{
    /// <summary>
    /// Gera um token de autenticação com base no id e role do usuário.
    /// </summary>
    /// <param name="id">Id do usuário</param>
    /// <param name="role">Perfil do usuário</param>
    /// <returns>Token de autenticação.</returns>
    string GenerateToken(Guid? id, string? role);
}