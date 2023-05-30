using Adapters.Gateways.Base;
using Adapters.Gateways.SubArea;
using Adapters.Interfaces;
using AutoMapper;
using Domain.Contracts.SubArea;
using Domain.Interfaces.UseCases;

namespace Adapters.PresenterController
{
    public class SubAreaPresenterController : ISubAreaPresenterController
    {
        #region Global Scope
        private readonly ICreateSubArea _createSubArea;
        private readonly IUpdateSubArea _updateSubArea;
        private readonly IDeleteSubArea _deleteSubArea;
        private readonly IGetSubAreasByArea _getSubAreasByArea;
        private readonly IGetSubAreaById _getSubAreaById;
        private readonly IMapper _mapper;

        public SubAreaPresenterController(ICreateSubArea createSubArea, IUpdateSubArea updateSubArea, IDeleteSubArea deleteSubArea,
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

        public async Task<IResponse> Create(IRequest request)
        {
            var dto = request as CreateSubAreaRequest;
            var input = _mapper.Map<CreateSubAreaInput>(dto);
            var result = await _createSubArea.Execute(input);
            return _mapper.Map<DetailedReadSubAreaResponse>(result);
        }

        public async Task<IResponse> Delete(Guid? id)
        {
            var result = await _deleteSubArea.Execute(id);
            return _mapper.Map<DetailedReadSubAreaResponse>(result);
        }

        public Task<IEnumerable<IResponse>> GetAll(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public async Task<IResponse> GetById(Guid? id)
        {
            var result = await _getSubAreaById.Execute(id);
            return _mapper.Map<DetailedReadSubAreaResponse>(result);
        }

        public async Task<IQueryable<IResponse>> GetSubAreasByArea(Guid? areaId, int skip, int take)
        {
            var result = await _getSubAreasByArea.Execute(areaId, skip, take);
            return _mapper.Map<IQueryable<ResumedReadSubAreaResponse>>(result);
        }

        public async Task<IResponse> Update(Guid? id, IRequest request)
        {
            var dto = request as UpdateSubAreaRequest;
            var input = _mapper.Map<UpdateSubAreaInput>(dto);
            var result = await _updateSubArea.Execute(id, input);
            return _mapper.Map<DetailedReadSubAreaResponse>(result);
        }
    }
}