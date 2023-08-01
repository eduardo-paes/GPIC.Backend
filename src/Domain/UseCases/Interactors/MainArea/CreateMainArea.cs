using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.MainArea;
using Domain.UseCases.Ports.MainArea;
using Domain.Validation;

namespace Domain.UseCases.Interactors.MainArea
{
    public class CreateMainArea : ICreateMainArea
    {
        #region Global Scope
        private readonly IMainAreaRepository _repository;
        private readonly IMapper _mapper;
        public CreateMainArea(IMainAreaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedMainAreaOutput> Execute(CreateMainAreaInput model)
        {
            // Validação de código da Área
            Entities.MainArea? entity = await _repository.GetByCode(model.Code);
            if (entity != null)
            {
                throw UseCaseException.BusinessRuleViolation($"Já existe uma Área Principal para o código {model.Code}");
            }

            entity = await _repository.Create(_mapper.Map<Entities.MainArea>(model));
            return _mapper.Map<DetailedMainAreaOutput>(entity);
        }
    }
}