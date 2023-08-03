using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.ProgramType;
using Domain.UseCases.Ports.ProgramType;
using Domain.Validation;

namespace Domain.UseCases.Interactors.ProgramType
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
            Entities.ProgramType? entity = await _repository.GetProgramTypeByName(model.Name!);
            if (entity != null)
            {
                throw UseCaseException.BusinessRuleViolation("Já existe um Tipo de Programa para o nome informado.");
            }

            // Cria entidade
            entity = await _repository.Create(_mapper.Map<Entities.ProgramType>(model));

            // Salva entidade no banco
            return _mapper.Map<DetailedReadProgramTypeOutput>(entity);
        }
    }
}