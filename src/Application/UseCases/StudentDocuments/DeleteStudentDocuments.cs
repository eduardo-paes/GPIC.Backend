using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.StudentDocuments;
using Application.Ports.StudentDocuments;
using Application.Validation;

namespace Application.UseCases.StudentDocuments
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
            // TODO: Verificar se seria preciso remover os documentos do aluno caso fosse removido o registro de documentos do aluno
            UseCaseException.NotInformedParam(id is null, nameof(id));
            var model = await _repository.DeleteAsync(id);
            return _mapper.Map<DetailedReadStudentDocumentsOutput>(model);
        }
    }
}