using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.StudentDocuments;
using Domain.UseCases.Ports.StudentDocuments;
using Domain.Validation;

namespace Domain.UseCases.Interactors.StudentDocuments
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

        public async Task<ResumedReadStudentDocumentsOutput> Execute(Guid? studentId)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(studentId is null, nameof(studentId));

            // Busca documentos do estudante pelo id do projeto
            Entities.StudentDocuments? entity = await _repository.GetByStudentId(studentId);

            // Retorna entidade mapeada
            return _mapper.Map<ResumedReadStudentDocumentsOutput>(entity);
        }
    }
}