using Domain.UseCases.Ports.Project;

namespace Domain.UseCases.Interfaces.Project
{
    public interface IOpenProject
    {
        Task<ResumedReadProjectOutput> ExecuteAsync(OpenProjectInput input);
    }
}