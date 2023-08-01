using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Retorna usuários ativos no sistema.
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns>Usuários encontrados.</returns>
        Task<IEnumerable<User>> GetActiveUsers(int skip, int take);

        /// <summary>
        /// Retorna usuários inativos no sistema.
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns>Usuários encontrados.</returns>
        Task<IEnumerable<User>> GetInactiveUsers(int skip, int take);

        /// <summary>
        /// Recupera usuário através do Id informado.
        /// </summary>
        /// <param name="id">Id do usuário.</param>
        /// <returns>Usuário encontrado.</returns>
        Task<User?> GetById(Guid? id);

        /// <summary>
        /// Atualiza usuário utilizando os parâmetros informados.
        /// </summary>
        /// <param name="user">Parâmetros de atualização do usuário.</param>
        /// <returns>Usuário atualizado.</returns>
        Task<User> Update(User user);

        /// <summary>
        /// Realiza a criação do usuário utilizando os parâmetros informados.
        /// </summary>
        /// <param name="user">Parâmetros de criação do usuário.</param>
        /// <returns>Usuário criado.</returns>
        Task<User> Create(User user);

        /// <summary>
        /// Realiza a remoção do usuário utilizando o id informado.
        /// </summary>
        /// <param name="id">Id do usuário.</param>
        /// <returns>Usuário removido.</returns>
        Task<User> Delete(Guid? id);

        /// <summary>
        /// Retorna usuário com o Email informado.
        /// Usuários inativos também são retornados.
        /// </summary>
        /// <param name="email">Email do usuário.</param>
        /// <returns>Usuário encontrado.</returns>
        Task<User?> GetUserByEmail(string? email);

        /// <summary>
        /// Retorna usuário com o CPF informado.
        /// Usuários inativos também são retornados.
        /// </summary>
        /// <param name="cpf">CPF do usuário.</param>
        /// <returns>Usuário encontrado.</returns>
        Task<User?> GetUserByCPF(string? cpf);
    }
}