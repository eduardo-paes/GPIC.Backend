using Application.Ports.Project;

namespace Application.Interfaces.UseCases.Project
{
    public interface IGetProjectsToEvaluate
    {
        /// <summary>
        /// Retorna todos os projetos que foram submetidos ou cuja documentação foi fornecida e estão aguardando avaliação.
        /// Os projetos retornados são apenas os que estão na fase de avaliação (Submitted, Evaluation, DocumentAnalysis).
        /// </summary>
        Task<IList<DetailedReadProjectOutput>> ExecuteAsync(int skip, int take);
    }
}