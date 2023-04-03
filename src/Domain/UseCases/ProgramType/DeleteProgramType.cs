using Domain.Contracts.ProgramType;
using Domain.Interfaces.UseCases.ProgramType;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases.ProgramType
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
        #endregion

        public async Task<DetailedReadProgramTypeOutput> Execute(Guid? id)
        {
            // Verifica se o id foi informado
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            // Remove a entidade
            var model = await _repository.Delete(id);

            // Retorna o tipo de programa removido
            return _mapper.Map<DetailedReadProgramTypeOutput>(model);
        }
    }
}