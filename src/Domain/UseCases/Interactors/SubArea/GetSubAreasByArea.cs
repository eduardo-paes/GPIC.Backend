using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.SubArea;
using Domain.UseCases.Ports.SubArea;
using Domain.Validation;

namespace Domain.UseCases.Interactors.SubArea
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
            IEnumerable<Entities.SubArea> entities = (IEnumerable<Entities.SubArea>)await _subAreaRepository.GetSubAreasByArea(areaId, skip, take);
            return _mapper.Map<IEnumerable<ResumedReadSubAreaOutput>>(entities).AsQueryable();
        }
    }
}