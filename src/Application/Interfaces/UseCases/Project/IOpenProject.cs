using Application.Ports.Project;

namespace Application.Interfaces.UseCases.Project
{
    public interface IOpenProject
    {
        Task<ResumedReadProjectOutput> ExecuteAsync(OpenProjectInput input);
    }
}