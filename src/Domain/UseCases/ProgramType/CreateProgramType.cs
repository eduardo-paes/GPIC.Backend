using Domain.Contracts.ProgramType;
using Domain.Interfaces.UseCases.ProgramType;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases.ProgramType
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
        #endregion

        public async Task<DetailedReadProgramTypeOutput> Execute(CreateProgramTypeInput dto)
        {
            // Verifica se nome foi informado
            if (string.IsNullOrEmpty(dto.Name))
                throw new ArgumentNullException(nameof(dto.Name));

            // Verifica se já existe um tipo de programa com o nome indicado
            var entity = await _repository.GetProgramTypeByName(dto.Name);
            if (entity != null)
                throw new Exception($"Já existe um Tipo de Programa para o nome informado.");

            // Cria entidade
            entity = await _repository.Create(_mapper.Map<Entities.ProgramType>(dto));

            // Salva entidade no banco
            return _mapper.Map<DetailedReadProgramTypeOutput>(entity);
        }
    }
}