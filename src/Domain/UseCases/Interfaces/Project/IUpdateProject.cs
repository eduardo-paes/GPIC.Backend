using Domain.UseCases.Ports.Project;

namespace Domain.UseCases.Interfaces.Project
{
    public interface IUpdateProject
    {
        Task<ResumedReadProjectOutput> Execute(Guid? id, UpdateProjectInput input);
    }
}