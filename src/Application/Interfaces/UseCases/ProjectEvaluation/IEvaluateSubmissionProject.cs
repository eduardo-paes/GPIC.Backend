using Application.Ports.Project;
using Application.Ports.ProjectEvaluation;

namespace Application.Interfaces.UseCases.ProjectEvaluation
{
    public interface IEvaluateSubmissionProject
    {
        /// <summary>
        /// Avalia o projeto submetido.
        /// </summary>
        /// <param name="input">Dados de entrada para a avaliação do projeto.</param>
        /// <returns>Projeto com a avaliação do projeto submetido.</returns>
        Task<DetailedReadProjectOutput> ExecuteAsync(EvaluateSubmissionProjectInput input);
    }
}