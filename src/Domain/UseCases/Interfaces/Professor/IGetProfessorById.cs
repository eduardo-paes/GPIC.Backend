using Domain.UseCases.Ports.Professor;

namespace Domain.UseCases.Interfaces.Professor
{
    public interface IGetProfessorById
    {
        Task<DetailedReadProfessorOutput> Execute(Guid? id);
    }
}