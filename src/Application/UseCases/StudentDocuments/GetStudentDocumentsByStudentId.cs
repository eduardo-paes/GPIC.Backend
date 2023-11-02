using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.StudentDocuments;
using Application.Ports.StudentDocuments;
using Application.Validation;

namespace Application.UseCases.StudentDocuments
{
    public class GetStudentDocumentsByStudentId : IGetStudentDocumentsByStudentId
    {
        #region Global Scope
        private readonly IStudentDocumentsRepository _repository;
        private readonly IMapper _mapper;
        public GetStudentDocumentsByStudentId(
            IStudentDocumentsRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<ResumedReadStudentDocumentsOutput> ExecuteAsync(Guid? studentId)
        {
            UseCaseException.NotInformedParam(studentId is null, nameof(studentId));
            Domain.Entities.StudentDocuments? entity = await _repository.GetByStudentIdAsync(studentId);
            return _mapper.Map<ResumedReadStudentDocumentsOutput>(entity);
        }
    }
}