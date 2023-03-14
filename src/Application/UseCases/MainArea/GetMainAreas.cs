using Application.DTOs.MainArea;
using Application.Proxies.MainArea;
using AutoMapper;
using Domain.Interfaces;

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
        #endregion

        public async Task<IQueryable<ReadMainAreaDTO>> Execute(int skip, int take)
        {
            var entities = await _repository.GetAll(skip, take);
            return _mapper.Map<IEnumerable<ReadMainAreaDTO>>(entities).AsQueryable();
        }
    }
}