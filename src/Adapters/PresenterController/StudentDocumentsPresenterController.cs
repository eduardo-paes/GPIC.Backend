using Adapters.Gateways.StudentDocuments;
using Adapters.Interfaces;
using AutoMapper;
using Domain.Contracts.StudentDocuments;
using Domain.Interfaces.UseCases;

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
        #endregion

        public async Task<DetailedReadStudentDocumentsResponse> Create(CreateStudentDocumentsRequest model)
        {
            var input = _mapper.Map<CreateStudentDocumentsInput>(model);
            var result = await _createStudentDocuments.Execute(input);
            return _mapper.Map<DetailedReadStudentDocumentsResponse>(result);
        }

        public async Task<DetailedReadStudentDocumentsResponse> Delete(Guid? id)
        {
            var result = await _deleteStudentDocuments.Execute(id);
            return _mapper.Map<DetailedReadStudentDocumentsResponse>(result);
        }

        public async Task<ResumedReadStudentDocumentsResponse> GetByProjectId(Guid? projectId)
        {
            var result = await _getStudentDocumentsByProject.Execute(projectId);
            return _mapper.Map<ResumedReadStudentDocumentsResponse>(result);
        }

        public async Task<ResumedReadStudentDocumentsResponse> GetByStudentId(Guid? studentId)
        {
            var result = await _getStudentDocumentsByStudent.Execute(studentId);
            return _mapper.Map<ResumedReadStudentDocumentsResponse>(result);
        }

        public async Task<DetailedReadStudentDocumentsResponse> Update(Guid? id, UpdateStudentDocumentsRequest model)
        {
            var input = _mapper.Map<UpdateStudentDocumentsInput>(model);
            var result = await _updateStudentDocuments.Execute(id, input);
            return _mapper.Map<DetailedReadStudentDocumentsResponse>(result);
        }
    }
}