using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.ProgramType;
using Application.Ports.ProgramType;
using Application.Validation;

namespace Application.UseCases.ProgramType
{
    public class CreateProgramType : ICreateProgramType
    {
        #region Global Scope
        private readonly IProgramTypeRepository _repository;
        private readonly IMapper _mapper;
        public CreateProgramType(IProgramTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadProgramTypeOutput> ExecuteAsync(CreateProgramTypeInput model)
        {
            // Verifica se nome foi informado
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(model.Name), nameof(model.Name));

            // Verifica se já existe um tipo de programa com o nome indicado
            var entity = await _repository.GetProgramTypeByNameAsync(model.Name!);
            UseCaseException.BusinessRuleViolation(entity != null, "Já existe um Tipo de Programa para o nome informado.");

            // Cria entidade
            entity = await _repository.CreateAsync(_mapper.Map<Domain.Entities.ProgramType>(model));

            // Salva entidade no banco
            return _mapper.Map<DetailedReadProgramTypeOutput>(entity);
        }
    }
}