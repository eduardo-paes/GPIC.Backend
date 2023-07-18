using Domain.Contracts.Campus;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Validation;

namespace Domain.UseCases
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
        #endregion

        public async Task<DetailedReadCampusOutput> Execute(Guid? id, UpdateCampusInput input)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Verifica se nome foi informado
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(input.Name), nameof(input.Name));

            // Recupera entidade que será atualizada
            var entity = await _repository.GetById(id) ?? throw new Exception("Campus não encontrado.");

            // Verifica se a entidade foi excluída
            if (entity.DeletedAt != null)
                throw UseCaseException.BusinessRuleViolation("O Campus informado já foi excluído.");

            // Verifica se o nome já está sendo usado
            if (!string.Equals(entity.Name, input.Name, StringComparison.OrdinalIgnoreCase) && await _repository.GetCampusByName(input.Name!) != null)
                throw UseCaseException.BusinessRuleViolation("Já existe um Campus para o nome informado.");

            // Atualiza atributos permitidos
            entity.Name = input.Name;

            // Salva entidade atualizada no banco
            var model = await _repository.Update(entity);
            return _mapper.Map<DetailedReadCampusOutput>(model);
        }
    }
}