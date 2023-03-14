using Application.DTOs.Area;
using Application.Proxies.Area;
using AutoMapper;
using Domain.Interfaces;

namespace Application.UseCases.Area
{
    public class GetAreaById : IGetAreaById
    {
        #region Global Scope
        private readonly IAreaRepository _repository;
        private readonly IMapper _mapper;
        public GetAreaById(IAreaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadAreaDTO> Execute(Guid? id)
        {
            var entity = await _repository.GetById(id);
            return _mapper.Map<DetailedReadAreaDTO>(entity);
        }
    }
}