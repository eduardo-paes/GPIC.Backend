using Domain.UseCases.Ports.Professor;

namespace Domain.UseCases.Interfaces.Professor
{
    public interface ICreateProfessor
    {
        Task<DetailedReadProfessorOutput> ExecuteAsync(CreateProfessorInput model);
    }
}