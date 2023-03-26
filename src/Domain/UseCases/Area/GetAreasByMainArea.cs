using Domain.Contracts.Area;
using Domain.Interfaces.UseCases.Area;
using AutoMapper;
using Domain.Interfaces.UseCases;

namespace Domain.UseCases.Area
{
    public class GetAreasByMainArea : IGetAreasByMainArea
    {
        #region Global Scope
        private readonly IAreaRepository _repository;
        private readonly IMapper _mapper;
        public GetAreasByMainArea(IAreaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<IQueryable<ResumedReadAreaOutput>> Execute(Guid? mainAreaId, int skip, int take)
        {
            var entities = await _repository.GetAreasByMainArea(mainAreaId, skip, take);
            return _mapper.Map<IEnumerable<ResumedReadAreaOutput>>(entities).AsQueryable();
        }
    }
}