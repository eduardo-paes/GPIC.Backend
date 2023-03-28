using Domain.Contracts.MainArea;
using Domain.Interfaces.UseCases.MainArea;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases.MainArea
{
    public class GetMainAreas : IGetMainAreas
    {
        #region Global Scope
        private readonly IMainAreaRepository _repository;
        private readonly IMapper _mapper;
        public GetMainAreas(IMainAreaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<IQueryable<ResumedReadMainAreaOutput>> Execute(int skip, int take)
        {
            var entities = await _repository.GetAll(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadMainAreaOutput>>(entities).AsQueryable();
        }
    }
}