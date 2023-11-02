using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.MainArea;
using Application.Ports.MainArea;
using Application.Validation;

namespace Application.UseCases.MainArea
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

        public async Task<DetailedMainAreaOutput> ExecuteAsync(CreateMainAreaInput model)
        {
            // Validação de código da Área
            var entity = await _repository.GetByCodeAsync(model.Code);
            UseCaseException.BusinessRuleViolation(entity != null, $"Já existe uma Área Principal para o código {model.Code}");

            entity = await _repository.CreateAsync(_mapper.Map<Domain.Entities.MainArea>(model));
            return _mapper.Map<DetailedMainAreaOutput>(entity);
        }
    }
}