using Application.Ports.Project;
using Application.Ports.ProjectEvaluation;

namespace Application.Interfaces.UseCases.ProjectEvaluation
{
    public interface IEvaluateAppealProject
    {
        Task<DetailedReadProjectOutput> ExecuteAsync(EvaluateAppealProjectInput input);
    }
}