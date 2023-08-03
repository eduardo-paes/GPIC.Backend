using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.Area;
using Domain.UseCases.Ports.Area;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Area
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
        #endregion Global Scope

        public async Task<IQueryable<ResumedReadAreaOutput>> ExecuteAsync(Guid? mainAreaId, int skip, int take)
        {
            UseCaseException.NotInformedParam(mainAreaId is null, nameof(mainAreaId));
            IEnumerable<Entities.Area> entities = (IEnumerable<Entities.Area>)await _repository.GetAreasByMainArea(mainAreaId, skip, take);
            return _mapper.Map<IEnumerable<ResumedReadAreaOutput>>(entities).AsQueryable();
        }
    }
}