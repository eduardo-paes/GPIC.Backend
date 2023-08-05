using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.SubArea;
using Application.Ports.SubArea;

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
        #endregion Global Scope

        public async Task<DetailedReadSubAreaOutput> ExecuteAsync(Guid? id)
        {
            Domain.Entities.SubArea? entity = await _subAreaRepository.GetByIdAsync(id);
            return _mapper.Map<DetailedReadSubAreaOutput>(entity);
        }
    }
}