using Domain.Contracts.Project;
using Domain.Contracts.ProjectEvaluation;

namespace Domain.Interfaces.UseCases.ProjectEvaluation
{
    public interface IEvaluateSubmissionProject
    {
        Task<DetailedReadProjectOutput> Execute(EvaluateSubmissionProjectInput input);
    }
}