using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.ProgramType;
using Domain.UseCases.Ports.ProgramType;
using Domain.Validation;

namespace Domain.UseCases.Interactors.ProgramType
{
    public class UpdateProgramType : IUpdateProgramType
    {
        #region Global Scope
        private readonly IProgramTypeRepository _repository;
        private readonly IMapper _mapper;
        public UpdateProgramType(IProgramTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadProgramTypeOutput> ExecuteAsync(Guid? id, UpdateProgramTypeInput input)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Verifica se nome foi informado
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(input.Name), nameof(input.Name));

            // Recupera entidade que será atualizada
            Entities.ProgramType entity = await _repository.GetByIdAsync(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.ProgramType));

            // Verifica se a entidade foi excluída
            UseCaseException.BusinessRuleViolation(entity.DeletedAt != null, "O Tipo de Programa informado já foi excluído.");

            // Verifica se o nome já está sendo usado
            UseCaseException.BusinessRuleViolation(
                !string.Equals(entity.Name, input.Name, StringComparison.OrdinalIgnoreCase)
                    && await _repository.GetProgramTypeByNameAsync(input.Name!) != null,
                "Já existe um Tipo de Programa para o nome informado.");

            // Atualiza atributos permitidos
            entity.Name = input.Name;
            entity.Description = input.Description;

            // Salva entidade atualizada no banco
            Entities.ProgramType model = await _repository.UpdateAsync(entity);
            return _mapper.Map<DetailedReadProgramTypeOutput>(model);
        }
    }
}