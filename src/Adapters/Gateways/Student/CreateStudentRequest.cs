using Adapters.Gateways.Base;
using Domain.UseCases.Ports.Student;

namespace Adapters.Gateways.Student
{
    public class CreateStudentRequest : CreateStudentInput, IRequest { }
}