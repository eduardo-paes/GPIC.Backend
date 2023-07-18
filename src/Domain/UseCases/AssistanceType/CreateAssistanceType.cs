using Domain.Contracts.AssistanceType;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Validation;

namespace Domain.UseCases
{
    public class CreateAssistanceType : ICreateAssistanceType
    {
        #region Global Scope
        private readonly IAssistanceTypeRepository _repository;
        private readonly IMapper _mapper;
        public CreateAssistanceType(IAssistanceTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadAssistanceTypeOutput> Execute(CreateAssistanceTypeInput input)
        {
            // Verifica se nome foi informado
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(input.Name), nameof(input.Name));

            // Verifica se já existe um tipo de programa com o nome indicado
            var entity = await _repository.GetAssistanceTypeByName(input.Name!);
            UseCaseException.BusinessRuleViolation(entity != null,
                "Já existe um Tipo de Programa para o nome informado.");

            // Cria entidade
            entity = await _repository.Create(_mapper.Map<Entities.AssistanceType>(input));

            // Salva entidade no banco
            return _mapper.Map<DetailedReadAssistanceTypeOutput>(entity);
        }
    }
}