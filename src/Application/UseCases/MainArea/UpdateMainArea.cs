using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.MainArea;
using Application.Ports.MainArea;
using Application.Validation;

namespace Application.UseCases.MainArea
{
    public class UpdateMainArea : IUpdateMainArea
    {
        #region Global Scope
        private readonly IMainAreaRepository _repository;
        private readonly IMapper _mapper;
        public UpdateMainArea(IMainAreaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedMainAreaOutput> ExecuteAsync(Guid? id, UpdateMainAreaInput input)
        {
            // Recupera entidade que será atualizada
            var entity = await _repository.GetByIdAsync(id)
                ?? throw UseCaseException.BusinessRuleViolation("Área Principal não encontrada.");

            // Atualiza atributos permitidos
            entity.Name = input.Name;
            entity.Code = input.Code;

            // Salva entidade atualizada no banco
            await _repository.UpdateAsync(entity);
            return _mapper.Map<DetailedMainAreaOutput>(entity);
        }
    }
}