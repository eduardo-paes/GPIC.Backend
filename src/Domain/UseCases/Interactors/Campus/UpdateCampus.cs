using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.Campus;
using Domain.UseCases.Ports.Campus;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Campus
{
    public class UpdateCampus : IUpdateCampus
    {
        #region Global Scope
        private readonly ICampusRepository _repository;
        private readonly IMapper _mapper;
        public UpdateCampus(ICampusRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadCampusOutput> ExecuteAsync(Guid? id, UpdateCampusInput input)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Verifica se nome foi informado
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(input.Name), nameof(input.Name));

            // Recupera entidade que será atualizada
            Entities.Campus entity = await _repository.GetByIdAsync(id) ?? throw new Exception("Campus não encontrado.");

            // Verifica se a entidade foi excluída
            if (entity.DeletedAt != null)
            {
                throw UseCaseException.BusinessRuleViolation("O Campus informado já foi excluído.");
            }

            // Verifica se o nome já está sendo usado
            if (!string.Equals(entity.Name, input.Name, StringComparison.OrdinalIgnoreCase) && await _repository.GetCampusByNameAsync(input.Name!) != null)
            {
                throw UseCaseException.BusinessRuleViolation("Já existe um Campus para o nome informado.");
            }

            // Atualiza atributos permitidos
            entity.Name = input.Name;

            // Salva entidade atualizada no banco
            Entities.Campus model = await _repository.UpdateAsync(entity);
            return _mapper.Map<DetailedReadCampusOutput>(model);
        }
    }
}