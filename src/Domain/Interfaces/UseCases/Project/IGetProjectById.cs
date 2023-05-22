using Domain.Contracts.Project;

namespace Domain.Interfaces.UseCases
{
    public interface IGetProjectById
    {
        Task<ResumedReadProjectOutput> Execute(Guid? id);
    }
}