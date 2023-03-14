using Application.DTOs.SubArea;
using Application.Proxies.SubArea;
using AutoMapper;
using Domain.Interfaces;

namespace Application.UseCases.SubArea
{
    public class DeleteSubArea : IDeleteSubArea
    {
        #region Global Scope
        private readonly ISubAreaRepository _subAreaRepository;
        private readonly IMapper _mapper;
        public DeleteSubArea(ISubAreaRepository subAreaRepository, IMapper mapper)
        {
            _subAreaRepository = subAreaRepository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadSubAreaDTO> Execute(Guid id)
        {
            var model = await _subAreaRepository.Delete(id);
            return _mapper.Map<DetailedReadSubAreaDTO>(model);
        }
    }
}