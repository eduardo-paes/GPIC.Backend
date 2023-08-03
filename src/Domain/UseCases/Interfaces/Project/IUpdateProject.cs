using Domain.UseCases.Ports.Project;

namespace Domain.UseCases.Interfaces.Project
{
    public interface IUpdateProject
    {
        Task<ResumedReadProjectOutput> ExecuteAsync(Guid? id, UpdateProjectInput input);
    }
}