using Domain.Contracts.StudentAssistanceScholarship;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases
{
    public class CreateStudentAssistanceScholarship : ICreateStudentAssistanceScholarship
    {
        #region Global Scope
        private readonly IStudentAssistanceScholarshipRepository _repository;
        private readonly IMapper _mapper;
        public CreateStudentAssistanceScholarship(IStudentAssistanceScholarshipRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadStudentAssistanceScholarshipOutput> Execute(CreateStudentAssistanceScholarshipInput input)
        {
            // Verifica se nome foi informado
            if (string.IsNullOrEmpty(input.Name))
                throw new ArgumentNullException(nameof(input.Name));

            // Verifica se já existe um tipo de programa com o nome indicado
            var entity = await _repository.GetStudentAssistanceScholarshipByName(input.Name);
            if (entity != null)
                throw new Exception($"Já existe um Tipo de Programa para o nome informado.");

            // Cria entidade
            entity = await _repository.Create(_mapper.Map<Entities.StudentAssistanceScholarship>(input));

            // Salva entidade no banco
            return _mapper.Map<DetailedReadStudentAssistanceScholarshipOutput>(entity);
        }
    }
}