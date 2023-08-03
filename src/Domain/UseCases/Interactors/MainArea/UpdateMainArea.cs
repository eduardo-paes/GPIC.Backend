using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.MainArea;
using Domain.UseCases.Ports.MainArea;

namespace Domain.UseCases.Interactors.MainArea
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
            Entities.MainArea entity = await _repository.GetById(id)
                ?? throw Validation.UseCaseException.BusinessRuleViolation("Área Principal não encontrada.");

            // Atualiza atributos permitidos
            entity.Name = input.Name;
            entity.Code = input.Code;

            // Salva entidade atualizada no banco
            Entities.MainArea model = await _repository.Update(entity);
            return _mapper.Map<DetailedMainAreaOutput>(model);
        }
    }
}