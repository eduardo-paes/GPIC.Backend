using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.SubArea;
using Domain.UseCases.Ports.SubArea;

namespace Domain.UseCases.Interactors.SubArea
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
            Entities.SubArea? entity = await _subAreaRepository.GetByIdAsync(id);
            return _mapper.Map<DetailedReadSubAreaOutput>(entity);
        }
    }
}