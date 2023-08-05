using Application.Ports.Project;

namespace Application.Interfaces.UseCases.Project
{
    public interface ISubmitProject
    {
        Task<ResumedReadProjectOutput> ExecuteAsync(Guid? projectId);
    }
}