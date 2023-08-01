using Domain.UseCases.Ports.ProjectEvaluation;

namespace Domain.UseCases.Interfaces.ProjectEvaluation
{
    public interface IGetEvaluationByProjectId
    {
        Task<DetailedReadProjectEvaluationOutput> Execute(Guid? projectId);
    }
}