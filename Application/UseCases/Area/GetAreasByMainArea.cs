using Application.DTOs.Area;
using Application.Proxies.Area;
using AutoMapper;
using Domain.Interfaces;

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
        #endregion

        public async Task<IQueryable<ResumedReadAreaDTO>> Execute(Guid? mainAreaId, int skip, int take)
        {
            var entities = await _repository.GetAreasByMainArea(mainAreaId, skip, take);
            return _mapper.Map<IEnumerable<ResumedReadAreaDTO>>(entities).AsQueryable();
        }
    }
}