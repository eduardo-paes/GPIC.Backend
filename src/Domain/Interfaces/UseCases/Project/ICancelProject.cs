using Domain.Contracts.Project;

namespace Domain.Interfaces.UseCases.Project
{
    public interface ICancelProject
    {
        Task<ResumedReadProjectOutput> Execute(Guid? id, string? observation);
    }
}