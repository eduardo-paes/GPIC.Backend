using Domain.UseCases.Ports.Student;

namespace Domain.UseCases.Interfaces.Student
{
    public interface IGetStudentByRegistrationCode
    {
        Task<DetailedReadStudentOutput> Execute(string? registrationCode);
    }
}