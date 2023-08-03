using Domain.UseCases.Ports.Project;

namespace Domain.UseCases.Interfaces.Project
{
    public interface IGetProjectById
    {
        Task<DetailedReadProjectOutput> ExecuteAsync(Guid? id);
    }
}