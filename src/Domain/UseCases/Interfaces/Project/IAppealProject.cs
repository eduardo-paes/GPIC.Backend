using Domain.UseCases.Ports.Project;

namespace Domain.UseCases.Interfaces.Project
{
    public interface IAppealProject
    {
        Task<ResumedReadProjectOutput> Execute(Guid? projectId, string? appealDescription);
    }
}