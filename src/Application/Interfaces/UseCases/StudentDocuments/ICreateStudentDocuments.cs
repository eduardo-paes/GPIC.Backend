using Application.Ports.StudentDocuments;

namespace Application.Interfaces.UseCases.StudentDocuments
{
    public interface ICreateStudentDocuments
    {
        Task<DetailedReadStudentDocumentsOutput> ExecuteAsync(CreateStudentDocumentsInput model);
    }
}