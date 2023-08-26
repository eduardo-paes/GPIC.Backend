using Application.Ports.Project;
using Application.Ports.ProjectEvaluation;

namespace Application.Interfaces.UseCases.ProjectEvaluation
{
    public interface IEvaluateStudentDocuments
    {
        /// <summary>
        /// Avalia os documentos informados pelo estudante associado ao projeto.
        /// </summary>
        /// <param name="input">Dados de entrada para a avaliação dos documentos.</param>
        /// <returns>Projeto com os documentos avaliados.</returns>
        Task<DetailedReadProjectOutput> ExecuteAsync(EvaluateStudentDocumentsInput input);
    }
}