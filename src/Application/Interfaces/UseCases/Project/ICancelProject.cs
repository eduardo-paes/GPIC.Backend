using Application.Ports.Project;

namespace Application.Interfaces.UseCases.Project
{
    public interface ICancelProject
    {
        Task<ResumedReadProjectOutput> ExecuteAsync(Guid? id, string? observation);
    }
}