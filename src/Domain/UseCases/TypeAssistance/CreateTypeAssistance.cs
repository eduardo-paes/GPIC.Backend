using Domain.Contracts.TypeAssistance;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Validation;

namespace Domain.UseCases
{
    public class CreateTypeAssistance : ICreateTypeAssistance
    {
        #region Global Scope
        private readonly ITypeAssistanceRepository _repository;
        private readonly IMapper _mapper;
        public CreateTypeAssistance(ITypeAssistanceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadTypeAssistanceOutput> Execute(CreateTypeAssistanceInput input)
        {
            // Verifica se nome foi informado
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(input.Name), nameof(input.Name));

            // Verifica se já existe um tipo de programa com o nome indicado
            var entity = await _repository.GetTypeAssistanceByName(input.Name!);
            UseCaseException.BusinessRuleViolation(entity != null,
                "Já existe um Tipo de Programa para o nome informado.");

            // Cria entidade
            entity = await _repository.Create(_mapper.Map<Entities.TypeAssistance>(input));

            // Salva entidade no banco
            return _mapper.Map<DetailedReadTypeAssistanceOutput>(entity);
        }
    }
}