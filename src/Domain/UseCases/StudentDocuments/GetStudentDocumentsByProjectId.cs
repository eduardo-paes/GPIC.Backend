using Domain.Contracts.StudentDocuments;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Validation;

namespace Domain.UseCases
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
        #endregion

        public async Task<ResumedReadStudentDocumentsOutput> Execute(Guid? projectId)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(projectId is null, nameof(projectId));

            // Busca documentos do estudante pelo id do projeto
            var entity = await _repository.GetByProjectId(projectId);

            // Retorna entidade mapeada
            return _mapper.Map<ResumedReadStudentDocumentsOutput>(entity);
        }
    }
}