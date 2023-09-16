using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.ProgramType;
using Application.Ports.ProgramType;

namespace Application.UseCases.ProgramType
{
    public class GetProgramTypes : IGetProgramTypes
    {
        #region Global Scope
        private readonly IProgramTypeRepository _repository;
        private readonly IMapper _mapper;
        public GetProgramTypes(IProgramTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<IQueryable<ResumedReadProgramTypeOutput>> ExecuteAsync(int skip, int take)
        {
            // Valida valores de skip e take
            if (skip < 0 || take < 1)
                throw new ArgumentException("Parâmetros inválidos.");

            var entities = await _repository.GetAllAsync(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadProgramTypeOutput>>(entities).AsQueryable();
        }
    }
}