using Application.Ports.Project;
using Application.Ports.ProjectEvaluation;

namespace Application.Interfaces.UseCases.ProjectEvaluation
{
    public interface IEvaluateSubmissionProject
    {
        Task<DetailedReadProjectOutput> ExecuteAsync(EvaluateSubmissionProjectInput input);
    }
}