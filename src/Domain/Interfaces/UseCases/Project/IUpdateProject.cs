using Domain.Contracts.Project;

namespace Domain.Interfaces.UseCases.Project
{
    public interface IUpdateProject
    {
        Task<ResumedReadProjectOutput> Execute(Guid? id, UpdateProjectInput input);
    }
}