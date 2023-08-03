using Adapters.Gateways.Student;
using Adapters.Interfaces.Base;

namespace Adapters.Interfaces;
public interface IStudentPresenterController : IGenericCRUDPresenterController
{
    Task<DetailedReadStudentResponse> GetByRegistrationCode(string? registrationCode);
    Task<string?> RequestStudentRegister(string? email);
}