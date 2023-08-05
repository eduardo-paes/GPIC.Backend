using Application.Ports.Project;

namespace Application.Interfaces.UseCases.Project
{
    public interface IAppealProject
    {
        Task<ResumedReadProjectOutput> ExecuteAsync(Guid? projectId, string? appealDescription);
    }
}