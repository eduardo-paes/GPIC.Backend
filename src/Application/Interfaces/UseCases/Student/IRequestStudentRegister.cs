namespace Application.Interfaces.UseCases.Student
{
    public interface IRequestStudentRegister
    {
        Task<string?> ExecuteAsync(string? email);
    }
}