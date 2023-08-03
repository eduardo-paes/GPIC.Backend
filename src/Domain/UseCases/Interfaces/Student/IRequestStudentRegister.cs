namespace Domain.UseCases.Interfaces.Student
{
    public interface IRequestStudentRegister
    {
        Task<string?> ExecuteAsync(string? email);
    }
}