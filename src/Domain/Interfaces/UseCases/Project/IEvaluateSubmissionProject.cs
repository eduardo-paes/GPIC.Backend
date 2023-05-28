using Domain.Contracts.Project;

namespace Domain.Interfaces.UseCases.Project
{
    public interface IEvaluateSubmissionProject
    {
        Task<DetailedReadProjectOutput> Execute(EvaluateSubmissionProjectInput input);
    }
}