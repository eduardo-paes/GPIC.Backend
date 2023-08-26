using Application.Ports.ProjectEvaluation;

namespace Application.Interfaces.UseCases.ProjectEvaluation
{
    public interface IGetEvaluationByProjectId
    {
        /// <summary>
        /// Obtém a avaliação do projeto.
        /// </summary>
        /// <param name="projectId">Identificador do projeto.</param>
        /// <returns>Avaliação do projeto.</returns> 
        Task<DetailedReadProjectEvaluationOutput> ExecuteAsync(Guid? projectId);
    }
}