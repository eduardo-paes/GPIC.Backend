using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.AssistanceType;
using Application.Ports.AssistanceType;
using Application.Validation;

namespace Application.UseCases.AssistanceType
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

        public async Task<DetailedReadAssistanceTypeOutput> ExecuteAsync(Guid? id, UpdateAssistanceTypeInput input)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Verifica se nome foi informado
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(input.Name), nameof(input.Name));

            // Recupera entidade que será atualizada
            var entity = await _repository.GetByIdAsync(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Domain.Entities.AssistanceType));

            // Verifica se a entidade foi excluída
            UseCaseException.BusinessRuleViolation(entity.DeletedAt != null,
                "O Bolsa de Assistência informado já foi excluído.");

            // Verifica se o nome já está sendo usado
            UseCaseException.BusinessRuleViolation(
                !string.Equals(entity.Name, input.Name, StringComparison.OrdinalIgnoreCase)
                    && await _repository.GetAssistanceTypeByNameAsync(input.Name!) != null,
                "Já existe um Bolsa de Assistência para o nome informado.");

            // Atualiza atributos permitidos
            entity.Name = input.Name;
            entity.Description = input.Description;

            // Salva entidade atualizada no banco
            var model = await _repository.UpdateAsync(entity);
            return _mapper.Map<DetailedReadAssistanceTypeOutput>(model);
        }
    }
}