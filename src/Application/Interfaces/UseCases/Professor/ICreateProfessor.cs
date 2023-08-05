using Application.Ports.Professor;

namespace Application.Interfaces.UseCases.Professor
{
    public interface ICreateProfessor
    {
        Task<DetailedReadProfessorOutput> ExecuteAsync(CreateProfessorInput model);
    }
}