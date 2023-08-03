using Domain.UseCases.Ports.StudentDocuments;

namespace Domain.UseCases.Interfaces.StudentDocuments
{
    public interface ICreateStudentDocuments
    {
        Task<DetailedReadStudentDocumentsOutput> ExecuteAsync(CreateStudentDocumentsInput model);
    }
}