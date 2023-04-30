using Adapters.DTOs.Area;
using Adapters.DTOs.Base;
using Adapters.Proxies.Area;
using AutoMapper;
using Domain.Contracts.Area;
using Domain.Interfaces.UseCases.Area;

namespace Adapters.Services
{
    public class AreaService : IAreaService
    {
        #region Global Scope
        private readonly ICreateArea _createArea;
        private readonly IUpdateArea _updateArea;
        private readonly IDeleteArea _deleteArea;
        private readonly IGetAreasByMainArea _getAreasByMainArea;
        private readonly IGetAreaById _getAreaById;
        private readonly IMapper _mapper;

        public AreaService(ICreateArea createArea, IUpdateArea updateArea, IDeleteArea deleteArea, IGetAreasByMainArea getAreasByMainArea, IGetAreaById getAreaById, IMapper mapper)
        {
            _createArea = createArea;
            _updateArea = updateArea;
            _deleteArea = deleteArea;
            _getAreasByMainArea = getAreasByMainArea;
            _getAreaById = getAreaById;
            _mapper = mapper;
        }
        #endregion

        public async Task<ResponseDTO> Create(RequestDTO model)
        {
            var dto = model as CreateAreaDTO;
            var input = _mapper.Map<CreateAreaInput>(dto);
            var result = await _createArea.Execute(input);
            return _mapper.Map<DetailedReadAreaDTO>(result);
        }

        public async Task<ResponseDTO> Delete(Guid? id)
        {
            var result = await _deleteArea.Execute(id);
            return _mapper.Map<DetailedReadAreaDTO>(result);
        }

        public async Task<IEnumerable<ResponseDTO>> GetAreasByMainArea(Guid? mainAreaId, int skip, int take)
        {
            var result = await _getAreasByMainArea.Execute(mainAreaId, skip, take);
            return _mapper.Map<IEnumerable<ResumedReadAreaDTO>>(result);
        }

        public Task<IEnumerable<ResponseDTO>> GetAll(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDTO> GetById(Guid? id)
        {
            var result = await _getAreaById.Execute(id);
            return _mapper.Map<DetailedReadAreaDTO>(result);
        }

        public async Task<ResponseDTO> Update(Guid? id, RequestDTO model)
        {
            var dto = model as UpdateAreaDTO;
            var input = _mapper.Map<UpdateAreaInput>(dto);
            var result = await _updateArea.Execute(id, input);
            return _mapper.Map<DetailedReadAreaDTO>(result);
        }
    }
}