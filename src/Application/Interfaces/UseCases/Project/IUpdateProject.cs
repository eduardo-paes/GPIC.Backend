using Application.Ports.Project;

namespace Application.Interfaces.UseCases.Project
{
    public interface IUpdateProject
    {
        Task<ResumedReadProjectOutput> ExecuteAsync(Guid? id, UpdateProjectInput input);
    }
}