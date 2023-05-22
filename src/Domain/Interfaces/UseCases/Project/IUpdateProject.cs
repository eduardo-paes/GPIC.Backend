using Domain.Contracts.Project;

namespace Domain.Interfaces.UseCases
{
    public interface IUpdateProject
    {
        Task<ResumedReadProjectOutput> Execute(UpdateProjectInput input);
    }
}