using Adapters.Gateways.Base;
using Adapters.Gateways.SubArea;
using Adapters.Interfaces;
using AutoMapper;
using Domain.UseCases.Interfaces.SubArea;
using Domain.UseCases.Ports.SubArea;

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
        #endregion Global Scope

        public async Task<IResponse> Create(IRequest request)
        {
            CreateSubAreaRequest? dto = request as CreateSubAreaRequest;
            CreateSubAreaInput input = _mapper.Map<CreateSubAreaInput>(dto);
            DetailedReadSubAreaOutput result = await _createSubArea.ExecuteAsync(input);
            return _mapper.Map<DetailedReadSubAreaResponse>(result);
        }

        public async Task<IResponse> Delete(Guid? id)
        {
            DetailedReadSubAreaOutput result = await _deleteSubArea.ExecuteAsync(id);
            return _mapper.Map<DetailedReadSubAreaResponse>(result);
        }

        public Task<IEnumerable<IResponse>> GetAll(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public async Task<IResponse> GetById(Guid? id)
        {
            DetailedReadSubAreaOutput result = await _getSubAreaById.ExecuteAsync(id);
            return _mapper.Map<DetailedReadSubAreaResponse>(result);
        }

        public async Task<IEnumerable<IResponse>> GetSubAreasByArea(Guid? areaId, int skip, int take)
        {
            IQueryable<ResumedReadSubAreaOutput> result = await _getSubAreasByArea.ExecuteAsync(areaId, skip, take);
            return _mapper.Map<IEnumerable<ResumedReadSubAreaResponse>>(result);
        }

        public async Task<IResponse> Update(Guid? id, IRequest request)
        {
            UpdateSubAreaRequest? dto = request as UpdateSubAreaRequest;
            UpdateSubAreaInput input = _mapper.Map<UpdateSubAreaInput>(dto);
            DetailedReadSubAreaOutput result = await _updateSubArea.ExecuteAsync(id, input);
            return _mapper.Map<DetailedReadSubAreaResponse>(result);
        }
    }
}