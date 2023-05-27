using Adapters.Gateways.Base;
using Adapters.Gateways.MainArea;
using Adapters.Interfaces;
using AutoMapper;
using Domain.Contracts.MainArea;
using Domain.Interfaces.UseCases;

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
        #endregion

        public async Task<Response> Create(Request request)
        {
            var dto = request as CreateMainAreaRequest;
            var input = _mapper.Map<CreateMainAreaInput>(dto);
            var result = await _createMainArea.Execute(input);
            return _mapper.Map<DetailedMainAreaResponse>(result);
        }

        public async Task<Response> Delete(Guid? id)
        {
            var result = await _deleteMainArea.Execute(id);
            return _mapper.Map<DetailedMainAreaResponse>(result);
        }

        public async Task<IEnumerable<Response>> GetAll(int skip, int take)
        {
            var result = await _getMainAreas.Execute(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadMainAreaResponse>>(result);
        }

        public async Task<Response> GetById(Guid? id)
        {
            var result = await _getMainAreaById.Execute(id);
            return _mapper.Map<DetailedMainAreaResponse>(result);
        }

        public async Task<Response> Update(Guid? id, Request request)
        {
            var dto = request as UpdateMainAreaRequest;
            var input = _mapper.Map<UpdateMainAreaInput>(dto);
            var result = await _updateMainArea.Execute(id, input);
            return _mapper.Map<DetailedMainAreaResponse>(result);
        }
    }
}