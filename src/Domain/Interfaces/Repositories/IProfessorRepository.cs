using Domain.Entities;
using Domain.Interfaces.Repositories.Bases;

namespace Domain.Interfaces.Repositories
{
    public interface IProfessorRepository : IGenericCRUDRepository<Professor>
    {
        /// <summary>
        /// Obtém todos os professores ativos.
        /// </summary>
        /// <returns>Lista de professores ativos.</returns>
        /// <remarks>Ativo significa que o professor não foi removido.</remarks>
        Task<IEnumerable<Professor>> GetAllActiveProfessorsAsync();

        /// <summary>
        /// Obtém professor pelo Id do usuário informado.
        /// </summary>
        /// <param name="userId">Id do usuário.</param>
        /// <returns>Professor encontrado.</returns>
        Task<Professor?> GetByUserIdAsync(Guid? userId);
    }
}