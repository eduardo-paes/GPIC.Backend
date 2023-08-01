using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.StudentDocuments;
using Domain.UseCases.Ports.StudentDocuments;
using Domain.Validation;

namespace Domain.UseCases.Interactors.StudentDocuments
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

        public async Task<ResumedReadStudentDocumentsOutput> Execute(Guid? projectId)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(projectId is null, nameof(projectId));

            // Busca documentos do estudante pelo id do projeto
            Entities.StudentDocuments? entity = await _repository.GetByProjectId(projectId);

            // Retorna entidade mapeada
            return _mapper.Map<ResumedReadStudentDocumentsOutput>(entity);
        }
    }
}