using Domain.Contracts.Project;

namespace Domain.Interfaces.UseCases.Project
{
    public interface ISubmitProject
    {
        Task<ResumedReadProjectOutput> Execute(Guid? projectId);
    }
}