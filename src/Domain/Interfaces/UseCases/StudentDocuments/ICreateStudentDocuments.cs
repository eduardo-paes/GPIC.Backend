using Domain.Contracts.StudentDocuments;

namespace Domain.Interfaces.UseCases
{
    public interface ICreateStudentDocuments
    {
        Task<DetailedReadStudentDocumentsOutput> Execute(CreateStudentDocumentsInput model);
    }
}