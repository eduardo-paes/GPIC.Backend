using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Retorna usuários ativos no sistema.
        /// </summary>
        /// <param name="skip">Quantidade de registros a serem ignorados.</param>
        /// <param name="take">Quantidade de registros a serem retornados.</param>
        /// <returns>Usuários encontrados.</returns>
        Task<IEnumerable<User>> GetActiveUsersAsync(int skip, int take);

        /// <summary>
        /// Retorna usuários inativos no sistema.
        /// </summary>
        /// <param name="skip">Quantidade de registros a serem ignorados.</param>
        /// <param name="take">Quantidade de registros a serem retornados.</param>
        /// <returns>Usuários encontrados.</returns>
        Task<IEnumerable<User>> GetInactiveUsersAsync(int skip, int take);

        /// <summary>
        /// Recupera usuário através do Id informado.
        /// </summary>
        /// <param name="id">Id do usuário.</param>
        /// <returns>Usuário encontrado.</returns>
        Task<User?> GetByIdAsync(Guid? id);

        /// <summary>
        /// Atualiza usuário utilizando os parâmetros informados.
        /// </summary>
        /// <param name="user">Parâmetros de atualização do usuário.</param>
        /// <returns>Usuário atualizado.</returns>
        Task<User> UpdateAsync(User user);

        /// <summary>
        /// Realiza a criação do usuário utilizando os parâmetros informados.
        /// </summary>
        /// <param name="user">Parâmetros de criação do usuário.</param>
        /// <returns>Usuário criado.</returns>
        Task<User> CreateAsync(User user);

        /// <summary>
        /// Realiza a remoção do usuário utilizando o id informado.
        /// </summary>
        /// <param name="id">Id do usuário.</param>
        /// <returns>Usuário removido.</returns>
        Task<User> DeleteAsync(Guid? id);

        /// <summary>
        /// Retorna usuário com o Email informado.
        /// Usuários inativos também são retornados.
        /// </summary>
        /// <param name="email">Email do usuário.</param>
        /// <returns>Usuário encontrado.</returns>
        Task<User?> GetUserByEmailAsync(string? email);

        /// <summary>
        /// Retorna usuário com o CPF informado.
        /// Usuários inativos também são retornados.
        /// </summary>
        /// <param name="cpf">CPF do usuário.</param>
        /// <returns>Usuário encontrado.</returns>
        Task<User?> GetUserByCPFAsync(string? cpf);

        /// <summary>
        /// Retorna usuário com permissão de coordenador.
        /// </summary>
        /// <returns>Coordenador encontrado.</returns>
        Task<User?> GetCoordinatorAsync();
    }
}