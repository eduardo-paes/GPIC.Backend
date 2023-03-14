using Application.DTOs.SubArea;
using Application.Proxies.SubArea;
using AutoMapper;
using Domain.Interfaces;

namespace Application.UseCases.SubArea
{
    public class GetSubAreaById : IGetSubAreaById
    {
        #region Global Scope
        private readonly ISubAreaRepository _subAreaRepository;
        private readonly IMapper _mapper;
        public GetSubAreaById(ISubAreaRepository subAreaRepository, IMapper mapper)
        {
            _subAreaRepository = subAreaRepository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadSubAreaDTO> Execute(Guid? id)
        {
            var entity = await _subAreaRepository.GetById(id);
            return _mapper.Map<DetailedReadSubAreaDTO>(entity);
        }
    }
}