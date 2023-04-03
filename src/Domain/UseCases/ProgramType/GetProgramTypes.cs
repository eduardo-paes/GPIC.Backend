using Domain.Contracts.ProgramType;
using Domain.Interfaces.UseCases.ProgramType;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases.ProgramType
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
        #endregion

        public async Task<IQueryable<ResumedReadProgramTypeOutput>> Execute(int skip, int take)
        {
            var entities = await _repository.GetAll(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadProgramTypeOutput>>(entities).AsQueryable();
        }
    }
}