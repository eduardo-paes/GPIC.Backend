using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.SubArea;
using Application.Ports.SubArea;
using Application.Validation;

namespace Application.UseCases.SubArea
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
        #endregion Global Scope

        public async Task<IQueryable<ResumedReadSubAreaOutput>> ExecuteAsync(Guid? areaId, int skip, int take)
        {
            UseCaseException.NotInformedParam(areaId is null, nameof(areaId));
            IEnumerable<Domain.Entities.SubArea> entities = await _subAreaRepository.GetSubAreasByAreaAsync(areaId, skip, take);
            return _mapper.Map<IEnumerable<ResumedReadSubAreaOutput>>(entities).AsQueryable();
        }
    }
}