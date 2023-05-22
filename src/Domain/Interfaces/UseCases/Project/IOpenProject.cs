using Domain.Contracts.Project;

namespace Domain.Interfaces.UseCases
{
    public interface IOpenProject
    {
        Task<ResumedReadProjectOutput> Execute(OpenProjectInput input);
    }
}