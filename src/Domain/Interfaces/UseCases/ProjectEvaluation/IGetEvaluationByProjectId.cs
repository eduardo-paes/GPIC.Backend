using Domain.Contracts.ProjectEvaluation;

namespace Domain.Interfaces.UseCases.ProjectEvaluation
{
    public interface IGetEvaluationByProjectId
    {
        Task<DetailedReadProjectEvaluationOutput> Execute(Guid? projectId);
    }
}