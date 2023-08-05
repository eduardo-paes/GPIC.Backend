using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Area;
using Application.Ports.Area;
using Application.Validation;

namespace Application.UseCases.Area
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
            var entities = await _repository.GetAreasByMainAreaAsync(mainAreaId, skip, take);
            return _mapper.Map<IEnumerable<ResumedReadAreaOutput>>(entities).AsQueryable();
        }
    }
}