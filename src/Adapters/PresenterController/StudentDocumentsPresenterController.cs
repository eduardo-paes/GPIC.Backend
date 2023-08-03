using Adapters.Gateways.StudentDocuments;
using Adapters.Interfaces;
using AutoMapper;
using Domain.UseCases.Interfaces.StudentDocuments;
using Domain.UseCases.Ports.StudentDocuments;

namespace Adapters.PresenterController
{
    public class StudentDocumentsPresenterController : IStudentDocumentsPresenterController
    {
        #region Global Scope
        private readonly ICreateStudentDocuments _createStudentDocuments;
        private readonly IUpdateStudentDocuments _updateStudentDocuments;
        private readonly IDeleteStudentDocuments _deleteStudentDocuments;
        private readonly IGetStudentDocumentsByProjectId _getStudentDocumentsByProject;
        private readonly IGetStudentDocumentsByStudentId _getStudentDocumentsByStudent;
        private readonly IMapper _mapper;
        public StudentDocumentsPresenterController(
            ICreateStudentDocuments createStudentDocuments,
            IUpdateStudentDocuments updateStudentDocuments,
            IDeleteStudentDocuments deleteStudentDocuments,
            IGetStudentDocumentsByProjectId getStudentDocumentsByProject,
            IGetStudentDocumentsByStudentId getStudentDocumentsByStudent,
            IMapper mapper)
        {
            _createStudentDocuments = createStudentDocuments;
            _updateStudentDocuments = updateStudentDocuments;
            _deleteStudentDocuments = deleteStudentDocuments;
            _getStudentDocumentsByProject = getStudentDocumentsByProject;
            _getStudentDocumentsByStudent = getStudentDocumentsByStudent;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadStudentDocumentsResponse> Create(CreateStudentDocumentsRequest model)
        {
            CreateStudentDocumentsInput input = _mapper.Map<CreateStudentDocumentsInput>(model);
            DetailedReadStudentDocumentsOutput result = await _createStudentDocuments.ExecuteAsync(input);
            return _mapper.Map<DetailedReadStudentDocumentsResponse>(result);
        }

        public async Task<DetailedReadStudentDocumentsResponse> Delete(Guid? id)
        {
            DetailedReadStudentDocumentsOutput result = await _deleteStudentDocuments.ExecuteAsync(id);
            return _mapper.Map<DetailedReadStudentDocumentsResponse>(result);
        }

        public async Task<ResumedReadStudentDocumentsResponse> GetByProjectId(Guid? projectId)
        {
            ResumedReadStudentDocumentsOutput result = await _getStudentDocumentsByProject.ExecuteAsync(projectId);
            return _mapper.Map<ResumedReadStudentDocumentsResponse>(result);
        }

        public async Task<ResumedReadStudentDocumentsResponse> GetByStudentId(Guid? studentId)
        {
            ResumedReadStudentDocumentsOutput result = await _getStudentDocumentsByStudent.ExecuteAsync(studentId);
            return _mapper.Map<ResumedReadStudentDocumentsResponse>(result);
        }

        public async Task<DetailedReadStudentDocumentsResponse> Update(Guid? id, UpdateStudentDocumentsRequest model)
        {
            UpdateStudentDocumentsInput input = _mapper.Map<UpdateStudentDocumentsInput>(model);
            DetailedReadStudentDocumentsOutput result = await _updateStudentDocuments.ExecuteAsync(id, input);
            return _mapper.Map<DetailedReadStudentDocumentsResponse>(result);
        }
    }
}