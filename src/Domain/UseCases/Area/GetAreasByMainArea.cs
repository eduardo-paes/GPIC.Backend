using Domain.Contracts.Area;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Validation;

namespace Domain.UseCases
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
            // Verifica se mainAreaId foi informado.
            UseCaseException.NotInformedParam(mainAreaId is null, nameof(mainAreaId));

            var entities = await _repository.GetAreasByMainArea(mainAreaId, skip, take);
            return _mapper.Map<IEnumerable<ResumedReadAreaOutput>>(entities).AsQueryable();
        }
    }
}