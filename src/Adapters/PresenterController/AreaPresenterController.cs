using Adapters.Gateways.Area;
using Adapters.Gateways.Base;
using Adapters.Interfaces;
using AutoMapper;
using Domain.Contracts.Area;
using Domain.Interfaces.UseCases;

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

        public AreaPresenterController(ICreateArea createArea,
            IUpdateArea updateArea,
            IDeleteArea deleteArea,
            IGetAreasByMainArea getAreasByMainArea,
            IGetAreaById getAreaById, IMapper mapper)
        {
            _createArea = createArea;
            _updateArea = updateArea;
            _deleteArea = deleteArea;
            _getAreasByMainArea = getAreasByMainArea;
            _getAreaById = getAreaById;
            _mapper = mapper;
        }
        #endregion

        public async Task<IResponse?> Create(IRequest request) => _mapper.Map<DetailedReadAreaResponse>(await _createArea.Execute((request as CreateAreaInput)!));
        public async Task<IResponse?> Delete(Guid? id) => _mapper.Map<DetailedReadAreaResponse>(await _deleteArea.Execute(id));
        public async Task<IEnumerable<IResponse>?> GetAreasByMainArea(Guid? mainAreaId, int skip, int take) => await _getAreasByMainArea.Execute(mainAreaId, skip, take) as IEnumerable<ResumedReadAreaResponse>;
        public Task<IEnumerable<IResponse>?> GetAll(int skip, int take) => throw new NotImplementedException();
        public async Task<IResponse?> GetById(Guid? id) => _mapper.Map<DetailedReadAreaResponse>(await _getAreaById.Execute(id));
        public async Task<IResponse?> Update(Guid? id, IRequest request) => _mapper.Map<DetailedReadAreaResponse>(await _updateArea.Execute(id, (request as UpdateAreaInput)!));
    }
}