using Domain.Contracts.Project;

namespace Domain.Interfaces.UseCases.Project
{
    public interface IAppealProject
    {
        Task<ResumedReadProjectOutput> Execute(Guid? projectId, string? appealDescription);
    }
}