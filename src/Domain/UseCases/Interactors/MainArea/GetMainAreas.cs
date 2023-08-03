using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.MainArea;
using Domain.UseCases.Ports.MainArea;

namespace Domain.UseCases.Interactors.MainArea
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
        #endregion Global Scope

        public async Task<IQueryable<ResumedReadMainAreaOutput>> ExecuteAsync(int skip, int take)
        {
            IEnumerable<Entities.MainArea> entities = (IEnumerable<Entities.MainArea>)await _repository.GetAllAsync(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadMainAreaOutput>>(entities).AsQueryable();
        }
    }
}