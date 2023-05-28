using Domain.Contracts.Project;

namespace Domain.Interfaces.UseCases
{
    public interface IGetProjectById
    {
        Task<DetailedReadProjectOutput> Execute(Guid? id);
    }
}