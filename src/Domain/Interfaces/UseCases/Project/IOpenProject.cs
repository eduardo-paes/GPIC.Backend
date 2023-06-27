using Domain.Contracts.Project;

namespace Domain.Interfaces.UseCases.Project
{
    public interface IOpenProject
    {
        Task<ResumedReadProjectOutput> Execute(OpenProjectInput input);
    }
}