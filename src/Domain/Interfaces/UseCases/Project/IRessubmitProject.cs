using Domain.Contracts.Project;

namespace Domain.Interfaces.UseCases.Project
{
    public interface IRessubmitProject
    {
        Task<ResumedReadProjectOutput> Execute(Guid? projectId, string? observation);
    }
}