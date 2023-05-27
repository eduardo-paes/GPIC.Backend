using Domain.Contracts.StudentAssistanceScholarship;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases
{
    public class DeleteStudentAssistanceScholarship : IDeleteStudentAssistanceScholarship
    {
        #region Global Scope
        private readonly IStudentAssistanceScholarshipRepository _repository;
        private readonly IMapper _mapper;
        public DeleteStudentAssistanceScholarship(IStudentAssistanceScholarshipRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadStudentAssistanceScholarshipOutput> Execute(Guid? id)
        {
            // Verifica se o id foi informado
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            // Remove a entidade
            var model = await _repository.Delete(id);

            // Retorna o tipo de programa removido
            return _mapper.Map<DetailedReadStudentAssistanceScholarshipOutput>(model);
        }
    }
}