using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.ProgramType;
using Domain.UseCases.Ports.ProgramType;

namespace Domain.UseCases.Interactors.ProgramType
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
            IEnumerable<Entities.ProgramType> entities = (IEnumerable<Entities.ProgramType>)await _repository.GetAllAsync(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadProgramTypeOutput>>(entities).AsQueryable();
        }
    }
}