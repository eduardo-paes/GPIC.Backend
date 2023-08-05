using Application.Ports.Professor;

namespace Application.Interfaces.UseCases.Professor
{
    public interface IGetProfessorById
    {
        Task<DetailedReadProfessorOutput> ExecuteAsync(Guid? id);
    }
}