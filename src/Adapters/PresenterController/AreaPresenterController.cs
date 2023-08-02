using Adapters.Gateways.Area;
using Adapters.Gateways.Base;
using Adapters.Interfaces;
using AutoMapper;
using Domain.UseCases.Interfaces.Area;
using Domain.UseCases.Ports.Area;

namespace Adapters.PresenterController
{
    public class AreaPresenterController : IAreaPresenterController
    {
        #region Global Scope
        private readonly ICreateArea _createArea;
        private readonly IUpdateArea _updateArea;
        private readonly IDeleteArea _deleteArea;
        private readonly IGetAreasByMainArea _getAreasByMainArea;
        private readonly IGetAreaById _getAreaById;
        private readonly IMapper _mapper;

        public AreaPresenterController(ICreateArea createArea, IUpdateArea updateArea, IDeleteArea deleteArea, IGetAreasByMainArea getAreasByMainArea, IGetAreaById getAreaById, IMapper mapper)
        {
            _createArea = createArea;
            _updateArea = updateArea;
            _deleteArea = deleteArea;
            _getAreasByMainArea = getAreasByMainArea;
            _getAreaById = getAreaById;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<IResponse> Create(IRequest request)
        {
            CreateAreaRequest? dto = request as CreateAreaRequest;
            CreateAreaInput input = _mapper.Map<CreateAreaInput>(dto);
            DetailedReadAreaOutput result = await _createArea.Execute(input);
            return _mapper.Map<DetailedReadAreaResponse>(result);
        }

        public async Task<IResponse> Delete(Guid? id)
        {
            DetailedReadAreaOutput result = await _deleteArea.Execute(id);
            return _mapper.Map<DetailedReadAreaResponse>(result);
        }

        public async Task<IEnumerable<IResponse>> GetAreasByMainArea(Guid? mainAreaId, int skip, int take)
        {
            IQueryable<ResumedReadAreaOutput> result = await _getAreasByMainArea.Execute(mainAreaId, skip, take);
            return _mapper.Map<IEnumerable<ResumedReadAreaResponse>>(result);
        }

        public Task<IEnumerable<IResponse>> GetAll(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public async Task<IResponse> GetById(Guid? id)
        {
            DetailedReadAreaOutput result = await _getAreaById.Execute(id);
            return _mapper.Map<DetailedReadAreaResponse>(result);
        }

        public async Task<IResponse> Update(Guid? id, IRequest request)
        {
            UpdateAreaRequest? dto = request as UpdateAreaRequest;
            UpdateAreaInput input = _mapper.Map<UpdateAreaInput>(dto);
            DetailedReadAreaOutput result = await _updateArea.Execute(id, input);
            return _mapper.Map<DetailedReadAreaResponse>(result);
        }
    }
}