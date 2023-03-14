using Application.DTOs.MainArea;
using Application.Proxies.MainArea;
using AutoMapper;
using Domain.Interfaces;

namespace Application.UseCases.MainArea
{
    public class GetMainAreaById : IGetMainAreaById
    {
        #region Global Scope
        private readonly IMainAreaRepository _repository;
        private readonly IMapper _mapper;
        public GetMainAreaById(IMainAreaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<ReadMainAreaDTO> Execute(Guid? id)
        {
            var entity = await _repository.GetById(id);
            return _mapper.Map<ReadMainAreaDTO>(entity);
        }
    }
}