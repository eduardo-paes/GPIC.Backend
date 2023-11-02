using Application.Ports.Project;
using Application.Ports.ProjectEvaluation;

namespace Application.Interfaces.UseCases.ProjectEvaluation
{
    public interface IEvaluateAppealProject
    {
        /// <summary>
        /// Avalia o recurso do projeto.
        /// </summary>
        /// <param name="input">Dados de entrada para a avaliação do recurso.</param>
        /// <returns>Projeto com o recurso avaliado.</returns>
        Task<DetailedReadProjectOutput> ExecuteAsync(EvaluateAppealProjectInput input);
    }
}