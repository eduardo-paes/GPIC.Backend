using Domain.Contracts.Project;
using Domain.Contracts.ProjectEvaluation;

namespace Domain.Interfaces.UseCases.ProjectEvaluation
{
    public interface IEvaluateAppealProject
    {
        Task<DetailedReadProjectOutput> Execute(EvaluateAppealProjectInput input);
    }
}