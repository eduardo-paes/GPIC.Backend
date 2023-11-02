using Application.Ports.Project;

namespace Application.Interfaces.UseCases.Project
{
    public interface IGetProjectById
    {
        Task<DetailedReadProjectOutput> ExecuteAsync(Guid? id);
    }
}