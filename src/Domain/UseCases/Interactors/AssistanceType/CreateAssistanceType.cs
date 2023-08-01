using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.AssistanceType;
using Domain.UseCases.Ports.AssistanceType;
using Domain.Validation;

namespace Domain.UseCases.Interactors.AssistanceType
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
        #endregion Global Scope

        public async Task<DetailedReadAssistanceTypeOutput> Execute(CreateAssistanceTypeInput model)
        {
            // Verifica se nome foi informado
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(model.Name), nameof(model.Name));

            // Verifica se já existe um tipo de programa com o nome indicado
            Entities.AssistanceType? entity = await _repository.GetAssistanceTypeByName(model.Name!);
            UseCaseException.BusinessRuleViolation(entity != null,
                "Já existe um Tipo de Programa para o nome informado.");

            // Cria entidade
            entity = await _repository.Create(_mapper.Map<Entities.AssistanceType>(model));

            // Salva entidade no banco
            return _mapper.Map<DetailedReadAssistanceTypeOutput>(entity);
        }
    }
}