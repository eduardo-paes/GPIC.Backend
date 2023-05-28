using Domain.Contracts.Project;

namespace Domain.Interfaces.UseCases.Project
{
    public interface IEvaluateAppealProject
    {
        Task<DetailedReadProjectOutput> Execute(EvaluateAppealProjectInput input);
    }
}