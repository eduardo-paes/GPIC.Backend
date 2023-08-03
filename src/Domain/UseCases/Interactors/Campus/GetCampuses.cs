using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.Campus;
using Domain.UseCases.Ports.Campus;

namespace Domain.UseCases.Interactors.Campus
{
    public class GetCampuses : IGetCampuses
    {
        #region Global Scope
        private readonly ICampusRepository _repository;
        private readonly IMapper _mapper;
        public GetCampuses(ICampusRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<IQueryable<ResumedReadCampusOutput>> ExecuteAsync(int skip, int take)
        {
            IEnumerable<Entities.Campus> entities = (IEnumerable<Entities.Campus>)await _repository.GetAll(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadCampusOutput>>(entities).AsQueryable();
        }
    }
}