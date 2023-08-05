using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.MainArea;
using Application.Ports.MainArea;

namespace Application.UseCases.MainArea
{
    public class GetMainAreas : IGetMainAreas
    {
        #region Global Scope
        private readonly IMainAreaRepository _repository;
        private readonly IMapper _mapper;
        public GetMainAreas(IMainAreaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<IQueryable<ResumedReadMainAreaOutput>> ExecuteAsync(int skip, int take)
        {
            var entities = await _repository.GetAllAsync(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadMainAreaOutput>>(entities).AsQueryable();
        }
    }
}