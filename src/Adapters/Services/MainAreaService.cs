using Adapters.DTOs.Base;
using Adapters.DTOs.MainArea;
using Adapters.Proxies;
using AutoMapper;
using Domain.Contracts.MainArea;
using Domain.Interfaces.UseCases;

namespace Adapters.Services
{
    public class MainAreaService : IMainAreaService
    {
        #region Global Scope
        private readonly ICreateMainArea _createMainArea;
        private readonly IUpdateMainArea _updateMainArea;
        private readonly IDeleteMainArea _deleteMainArea;
        private readonly IGetMainAreas _getMainAreas;
        private readonly IGetMainAreaById _getMainAreaById;
        private readonly IMapper _mapper;

        public MainAreaService(ICreateMainArea createMainArea, IUpdateMainArea updateMainArea, IDeleteMainArea deleteMainArea, IGetMainAreas getMainAreas, IGetMainAreaById getMainAreaById, IMapper mapper)
        {
            _createMainArea = createMainArea;
            _updateMainArea = updateMainArea;
            _deleteMainArea = deleteMainArea;
            _getMainAreas = getMainAreas;
            _getMainAreaById = getMainAreaById;
            _mapper = mapper;
        }
        #endregion

        public async Task<ResponseDTO> Create(RequestDTO model)
        {
            var dto = model as CreateMainAreaDTO;
            var input = _mapper.Map<CreateMainAreaInput>(dto);
            var result = await _createMainArea.Execute(input);
            return _mapper.Map<DetailedMainAreaDTO>(result);
        }

        public async Task<ResponseDTO> Delete(Guid? id)
        {
            var result = await _deleteMainArea.Execute(id);
            return _mapper.Map<DetailedMainAreaDTO>(result);
        }

        public async Task<IEnumerable<ResponseDTO>> GetAll(int skip, int take)
        {
            var result = await _getMainAreas.Execute(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadMainAreaDTO>>(result);
        }

        public async Task<ResponseDTO> GetById(Guid? id)
        {
            var result = await _getMainAreaById.Execute(id);
            return _mapper.Map<DetailedMainAreaDTO>(result);
        }

        public async Task<ResponseDTO> Update(Guid? id, RequestDTO model)
        {
            var dto = model as UpdateMainAreaDTO;
            var input = _mapper.Map<UpdateMainAreaInput>(dto);
            var result = await _updateMainArea.Execute(id, input);
            return _mapper.Map<DetailedMainAreaDTO>(result);
        }
    }
}