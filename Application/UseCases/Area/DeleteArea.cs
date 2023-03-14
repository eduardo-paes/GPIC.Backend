using Application.DTOs.Area;
using Application.Proxies.Area;
using AutoMapper;
using Domain.Interfaces;

namespace Application.UseCases.Area
{
    public class DeleteArea : IDeleteArea
    {
        #region Global Scope
        private readonly IAreaRepository _repository;
        private readonly IMapper _mapper;
        public DeleteArea(IAreaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadAreaDTO> Execute(Guid id)
        {
            var model = await _repository.Delete(id);
            return _mapper.Map<DetailedReadAreaDTO>(model);
        }
    }
}