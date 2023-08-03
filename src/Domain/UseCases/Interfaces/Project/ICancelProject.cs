using Domain.UseCases.Ports.Project;

namespace Domain.UseCases.Interfaces.Project
{
    public interface ICancelProject
    {
        Task<ResumedReadProjectOutput> ExecuteAsync(Guid? id, string? observation);
    }
}