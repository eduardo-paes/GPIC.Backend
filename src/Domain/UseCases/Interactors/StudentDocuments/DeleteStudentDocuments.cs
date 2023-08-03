using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.StudentDocuments;
using Domain.UseCases.Ports.StudentDocuments;
using Domain.Validation;

namespace Domain.UseCases.Interactors.StudentDocuments
{
    public class DeleteStudentDocuments : IDeleteStudentDocuments
    {
        #region Global Scope
        private readonly IStudentDocumentsRepository _repository;
        private readonly IMapper _mapper;
        public DeleteStudentDocuments(IStudentDocumentsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadStudentDocumentsOutput> ExecuteAsync(Guid? id)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Remove a entidade
            Entities.StudentDocuments model = await _repository.Delete(id);

            // TODO: Verificar se seria preciso remover os documentos do aluno caso fosse removido o registro de documentos do aluno

            // Retorna o tipo de programa removido
            return _mapper.Map<DetailedReadStudentDocumentsOutput>(model);
        }
    }
}