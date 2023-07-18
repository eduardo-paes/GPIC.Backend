using Domain.Contracts.SubArea;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;

namespace Domain.UseCases
{
    public class GetSubAreasByArea : IGetSubAreasByArea
    {
        #region Global Scope
        private readonly ISubAreaRepository _subAreaRepository;
        private readonly IMapper _mapper;
        public GetSubAreasByArea(ISubAreaRepository subAreaRepository, IMapper mapper)
        {
            _subAreaRepository = subAreaRepository;
            _mapper = mapper;
        }
        #endregion

        public async Task<IQueryable<ResumedReadSubAreaOutput>> Execute(Guid? areaId, int skip, int take)
        {
            var entities = await _subAreaRepository.GetSubAreasByArea(areaId, skip, take);
            return _mapper.Map<IEnumerable<ResumedReadSubAreaOutput>>(entities).AsQueryable();
        }
    }
}