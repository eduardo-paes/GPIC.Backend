using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.StudentDocuments;
using Application.Ports.StudentDocuments;
using Application.Validation;

namespace Application.UseCases.StudentDocuments
{
    public class GetStudentDocumentsByProjectId : IGetStudentDocumentsByProjectId
    {
        #region Global Scope
        private readonly IStudentDocumentsRepository _repository;
        private readonly IMapper _mapper;
        public GetStudentDocumentsByProjectId(
            IStudentDocumentsRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<ResumedReadStudentDocumentsOutput> ExecuteAsync(Guid? projectId)
        {
            UseCaseException.NotInformedParam(projectId is null, nameof(projectId));
            Domain.Entities.StudentDocuments? entity = await _repository.GetByProjectIdAsync(projectId);
            return _mapper.Map<ResumedReadStudentDocumentsOutput>(entity);
        }
    }
}