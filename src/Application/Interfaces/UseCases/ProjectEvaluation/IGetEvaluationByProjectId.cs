using Application.Ports.ProjectEvaluation;

namespace Application.Interfaces.UseCases.ProjectEvaluation
{
    public interface IGetEvaluationByProjectId
    {
        Task<DetailedReadProjectEvaluationOutput> ExecuteAsync(Guid? projectId);
    }
}