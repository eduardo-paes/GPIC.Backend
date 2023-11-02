using Domain.Entities;
using Domain.Interfaces.Repositories.Bases;

namespace Domain.Interfaces.Repositories
{
    public interface IProjectPartialReportRepository : IGenericCRUDRepository<ProjectPartialReport>
    {
        /// <summary>
        /// Busca relatório parcial de projeto pelo Id do projeto informado.
        /// </summary>
        /// <param name="projectId">Id do projeto.</param>
        /// <returns>Relatório parcial de projeto encontrado.</returns>
        Task<ProjectPartialReport?> GetByProjectIdAsync(Guid? projectId);
    }
}