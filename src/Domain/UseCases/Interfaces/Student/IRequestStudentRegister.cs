namespace Domain.UseCases.Interfaces.Student
{
    public interface IRequestStudentRegister
    {
        Task<string?> Execute(string? email);
    }
}