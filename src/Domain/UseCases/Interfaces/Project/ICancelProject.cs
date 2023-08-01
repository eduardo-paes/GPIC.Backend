using Domain.UseCases.Ports.Project;

namespace Domain.UseCases.Interfaces.Project
{
    public interface ICancelProject
    {
        Task<ResumedReadProjectOutput> Execute(Guid? id, string? observation);
    }
}