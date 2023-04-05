using Domain.Contracts.Campus;
using Domain.Interfaces.UseCases.Campus;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases.Campus
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
        #endregion

        public async Task<IQueryable<ResumedReadCampusOutput>> Execute(int skip, int take)
        {
            var entities = await _repository.GetAll(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadCampusOutput>>(entities).AsQueryable();
        }
    }
}