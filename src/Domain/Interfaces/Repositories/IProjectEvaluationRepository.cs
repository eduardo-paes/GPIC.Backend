using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IProjectEvaluationRepository
    {
        /// <summary>
        /// Busca uma avaliação de projeto pelo id.
        /// </summary>
        /// <param name="id">Id da avaliação.</param>
        /// <returns>Avaliação de projeto encontrado.</returns>
        Task<ProjectEvaluation?> GetById(Guid? id);

        /// <summary>
        /// Busca uma avaliação de projeto pelo id do projeto.
        /// </summary>
        /// <param name="projectId">Id do projeto em avaliação.</param>
        /// <returns>Avaliação de projeto encontrado.</returns>
        Task<ProjectEvaluation?> GetByProjectId(Guid? projectId);

        /// <summary>
        /// Cria uma avaliação de projeto.
        /// </summary>
        /// <param name="model">Modelo da avaliação de projeto.</param>
        /// <returns>Avaliação de projeto criado.</returns>
        Task<ProjectEvaluation> Create(ProjectEvaluation model);

        /// <summary>
        /// Atualiza uma avaliação de projeto.
        /// </summary>
        /// <param name="model">Modelo da avaliação de projeto.</param>
        /// <returns>Avaliação de projeto atualizado.</returns>
        Task<ProjectEvaluation> Update(ProjectEvaluation model);
    }
}