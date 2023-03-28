using Adapters.DTOs.Base;
using Adapters.DTOs.SubArea;
using Adapters.Proxies.SubArea;
using AutoMapper;
using Domain.Contracts.SubArea;
using Domain.Interfaces.UseCases.SubArea;

namespace Adapters.Services
{
    public class SubAreaService : ISubAreaService
    {
        #region Global Scope
        private readonly ICreateSubArea _createSubArea;
        private readonly IUpdateSubArea _updateSubArea;
        private readonly IDeleteSubArea _deleteSubArea;
        private readonly IGetSubAreasByArea _getSubAreasByArea;
        private readonly IGetSubAreaById _getSubAreaById;
        private readonly IMapper _mapper;

        public SubAreaService(ICreateSubArea createSubArea, IUpdateSubArea updateSubArea, IDeleteSubArea deleteSubArea,
        IGetSubAreasByArea getSubAreasByArea, IGetSubAreaById getSubAreaById, IMapper mapper)
        {
            _createSubArea = createSubArea;
            _updateSubArea = updateSubArea;
            _deleteSubArea = deleteSubArea;
            _getSubAreasByArea = getSubAreasByArea;
            _getSubAreaById = getSubAreaById;
            _mapper = mapper;
        }
        #endregion

        public async Task<ResponseDTO> Create(RequestDTO model)
        {
            var dto = model as CreateSubAreaDTO;
            var input = _mapper.Map<CreateSubAreaInput>(dto);
            var result = await _createSubArea.Execute(input);
            return _mapper.Map<DetailedReadSubAreaDTO>(result);
        }

        public async Task<ResponseDTO> Delete(Guid? id)
        {
            var result = await _deleteSubArea.Execute(id);
            return _mapper.Map<DetailedReadSubAreaDTO>(result);
        }

        public Task<IEnumerable<ResponseDTO>> GetAll(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDTO> GetById(Guid? id)
        {
            var result = await _getSubAreaById.Execute(id);
            return _mapper.Map<DetailedReadSubAreaDTO>(result);
        }

        public async Task<IQueryable<ResponseDTO>> GetSubAreasByArea(Guid? areaId, int skip, int take)
        {
            var result = await _getSubAreasByArea.Execute(areaId, skip, take);
            return _mapper.Map<IQueryable<ResumedReadSubAreaDTO>>(result);
        }

        public async Task<ResponseDTO> Update(Guid? id, RequestDTO model)
        {
            var dto = model as UpdateSubAreaDTO;
            var input = _mapper.Map<UpdateSubAreaInput>(dto);
            var result = await _updateSubArea.Execute(id, input);
            return _mapper.Map<DetailedReadSubAreaDTO>(result);
        }
    }
}