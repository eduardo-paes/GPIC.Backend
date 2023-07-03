using Adapters.Gateways.StudentDocuments;

namespace Adapters.Interfaces;
public interface IStudentDocumentsPresenterController
{
    Task<DetailedReadStudentDocumentsResponse> Create(CreateStudentDocumentsRequest model);
    Task<DetailedReadStudentDocumentsResponse> Delete(Guid? id);
    Task<ResumedReadStudentDocumentsResponse> GetByProjectId(Guid? projectId);
    Task<ResumedReadStudentDocumentsResponse> GetByStudentId(Guid? studentId);
    Task<DetailedReadStudentDocumentsResponse> Update(Guid? id, UpdateStudentDocumentsRequest model);
}