using Domain.UseCases.Ports.Project;

namespace Domain.UseCases.Interfaces.Project
{
    public interface IAppealProject
    {
        Task<ResumedReadProjectOutput> ExecuteAsync(Guid? projectId, string? appealDescription);
    }
}