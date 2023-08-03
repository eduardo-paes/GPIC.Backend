using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.ProgramType;
using Domain.UseCases.Ports.ProgramType;
using Domain.Validation;

namespace Domain.UseCases.Interactors.ProgramType
{
    public class DeleteProgramType : IDeleteProgramType
    {
        #region Global Scope
        private readonly IProgramTypeRepository _repository;
        private readonly IMapper _mapper;
        public DeleteProgramType(IProgramTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadProgramTypeOutput> ExecuteAsync(Guid? id)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Remove a entidade
            Entities.ProgramType model = await _repository.DeleteAsync(id);

            // Retorna o tipo de programa removido
            return _mapper.Map<DetailedReadProgramTypeOutput>(model);
        }
    }
}