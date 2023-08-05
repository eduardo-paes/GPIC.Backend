using Application.Ports.Student;

namespace Application.Interfaces.UseCases.Student
{
    public interface IGetStudentByRegistrationCode
    {
        Task<DetailedReadStudentOutput> ExecuteAsync(string? registrationCode);
    }
}