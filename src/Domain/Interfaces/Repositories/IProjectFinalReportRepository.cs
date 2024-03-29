using Domain.Entities;
using Domain.Interfaces.Repositories.Bases;

namespace Domain.Interfaces.Repositories
{
    public interface IProjectFinalReportRepository : IGenericCRUDRepository<ProjectFinalReport>
    {
        /// <summary>
        /// Busca relatório de projeto pelo Id do projeto informado.
        /// </summary>
        /// <param name="projectId">Id do projeto.</param>
        /// <returns>Relatório de projeto encontrado.</returns>
        Task<ProjectFinalReport?> GetByProjectIdAsync(Guid? projectId);
    }
}