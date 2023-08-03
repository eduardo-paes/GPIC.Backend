using Domain.UseCases.Ports.Project;
using Domain.UseCases.Ports.ProjectEvaluation;

namespace Domain.UseCases.Interfaces.ProjectEvaluation
{
    public interface IEvaluateAppealProject
    {
        Task<DetailedReadProjectOutput> ExecuteAsync(EvaluateAppealProjectInput input);
    }
}