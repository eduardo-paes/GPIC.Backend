using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.AssistanceType;
using Domain.UseCases.Ports.AssistanceType;
using Domain.Validation;

namespace Domain.UseCases.Interactors.AssistanceType
{
    public class UpdateAssistanceType : IUpdateAssistanceType
    {
        #region Global Scope
        private readonly IAssistanceTypeRepository _repository;
        private readonly IMapper _mapper;
        public UpdateAssistanceType(IAssistanceTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadAssistanceTypeOutput> Execute(Guid? id, UpdateAssistanceTypeInput input)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Verifica se nome foi informado
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(input.Name), nameof(input.Name));

            // Recupera entidade que será atualizada
            Entities.AssistanceType entity = await _repository.GetById(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.AssistanceType));

            // Verifica se a entidade foi excluída
            UseCaseException.BusinessRuleViolation(entity.DeletedAt != null,
                "O Bolsa de Assistência informado já foi excluído.");

            // Verifica se o nome já está sendo usado
            UseCaseException.BusinessRuleViolation(
                !string.Equals(entity.Name, input.Name, StringComparison.OrdinalIgnoreCase)
                    && await _repository.GetAssistanceTypeByName(input.Name!) != null,
                "Já existe um Bolsa de Assistência para o nome informado.");

            // Atualiza atributos permitidos
            entity.Name = input.Name;
            entity.Description = input.Description;

            // Salva entidade atualizada no banco
            Entities.AssistanceType model = await _repository.Update(entity);
            return _mapper.Map<DetailedReadAssistanceTypeOutput>(model);
        }
    }
}