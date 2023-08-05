using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.AssistanceType;
using Application.Ports.AssistanceType;
using Application.Validation;

namespace Application.UseCases.AssistanceType
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

        public async Task<DetailedReadAssistanceTypeOutput> ExecuteAsync(CreateAssistanceTypeInput model)
        {
            // Verifica se nome foi informado
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(model.Name), nameof(model.Name));

            // Verifica se já existe um tipo de programa com o nome indicado
            var entity = await _repository.GetAssistanceTypeByNameAsync(model.Name!);
            UseCaseException.BusinessRuleViolation(entity != null,
                "Já existe um Tipo de Programa para o nome informado.");

            // Cria entidade
            entity = await _repository.CreateAsync(_mapper.Map<Domain.Entities.AssistanceType>(model));

            // Salva entidade no banco
            return _mapper.Map<DetailedReadAssistanceTypeOutput>(entity);
        }
    }
}