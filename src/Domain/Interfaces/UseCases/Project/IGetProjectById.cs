using Domain.Contracts.Project;

namespace Domain.Interfaces.UseCases.Project
{
    public interface IGetProjectById
    {
        Task<DetailedReadProjectOutput> Execute(Guid? id);
    }
}