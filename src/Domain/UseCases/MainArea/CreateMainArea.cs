using Domain.Contracts.MainArea;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using System.Threading.Tasks;
using System;
using Domain.Validation;

namespace Domain.UseCases
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
        #endregion

        public async Task<DetailedMainAreaOutput> Execute(CreateMainAreaInput input)
        {
            // Validação de código da Área
            var entity = await _repository.GetByCode(input.Code);
            if (entity != null)
                throw UseCaseException.BusinessRuleViolation($"Já existe uma Área Principal para o código {input.Code}");

            entity = await _repository.Create(_mapper.Map<Entities.MainArea>(input));
            return _mapper.Map<DetailedMainAreaOutput>(entity);
        }
    }
}