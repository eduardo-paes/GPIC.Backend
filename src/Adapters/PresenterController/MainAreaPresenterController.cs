using Adapters.Gateways.Base;
using Adapters.Gateways.MainArea;
using Adapters.Interfaces;
using AutoMapper;
using Domain.UseCases.Interfaces.MainArea;
using Domain.UseCases.Ports.MainArea;

namespace Adapters.PresenterController
{
    public class MainAreaPresenterController : IMainAreaPresenterController
    {
        #region Global Scope
        private readonly ICreateMainArea _createMainArea;
        private readonly IUpdateMainArea _updateMainArea;
        private readonly IDeleteMainArea _deleteMainArea;
        private readonly IGetMainAreas _getMainAreas;
        private readonly IGetMainAreaById _getMainAreaById;
        private readonly IMapper _mapper;

        public MainAreaPresenterController(ICreateMainArea createMainArea, IUpdateMainArea updateMainArea, IDeleteMainArea deleteMainArea, IGetMainAreas getMainAreas, IGetMainAreaById getMainAreaById, IMapper mapper)
        {
            _createMainArea = createMainArea;
            _updateMainArea = updateMainArea;
            _deleteMainArea = deleteMainArea;
            _getMainAreas = getMainAreas;
            _getMainAreaById = getMainAreaById;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<IResponse> Create(IRequest request)
        {
            CreateMainAreaRequest? dto = request as CreateMainAreaRequest;
            CreateMainAreaInput input = _mapper.Map<CreateMainAreaInput>(dto);
            DetailedMainAreaOutput result = await _createMainArea.ExecuteAsync(input);
            return _mapper.Map<DetailedReadMainAreaResponse>(result);
        }

        public async Task<IResponse> Delete(Guid? id)
        {
            DetailedMainAreaOutput result = await _deleteMainArea.ExecuteAsync(id);
            return _mapper.Map<DetailedReadMainAreaResponse>(result);
        }

        public async Task<IEnumerable<IResponse>> GetAll(int skip, int take)
        {
            IQueryable<ResumedReadMainAreaOutput> result = await _getMainAreas.ExecuteAsync(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadMainAreaResponse>>(result);
        }

        public async Task<IResponse> GetById(Guid? id)
        {
            DetailedMainAreaOutput result = await _getMainAreaById.ExecuteAsync(id);
            return _mapper.Map<DetailedReadMainAreaResponse>(result);
        }

        public async Task<IResponse> Update(Guid? id, IRequest request)
        {
            UpdateMainAreaRequest? dto = request as UpdateMainAreaRequest;
            UpdateMainAreaInput input = _mapper.Map<UpdateMainAreaInput>(dto);
            DetailedMainAreaOutput result = await _updateMainArea.ExecuteAsync(id, input);
            return _mapper.Map<DetailedReadMainAreaResponse>(result);
        }
    }
}