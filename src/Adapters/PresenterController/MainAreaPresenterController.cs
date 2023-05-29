using Adapters.Gateways.Base;
using Adapters.Gateways.MainArea;
using Adapters.Interfaces;
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
        private readonly IGetMainAreas _getMainAreaes;
        private readonly IGetMainAreaById _getMainAreaById;

        public MainAreaPresenterController(ICreateMainArea createMainArea,
            IUpdateMainArea updateMainArea,
            IDeleteMainArea deleteMainArea,
            IGetMainAreas getMainAreaes,
            IGetMainAreaById getMainAreaById)
        {
            _createMainArea = createMainArea;
            _updateMainArea = updateMainArea;
            _deleteMainArea = deleteMainArea;
            _getMainAreaes = getMainAreaes;
            _getMainAreaById = getMainAreaById;
        }
        #endregion

        public async Task<IResponse?> Create(IRequest request) => await _createMainArea.Execute((request as CreateMainAreaInput)!) as DetailedReadMainAreaResponse;
        public async Task<IResponse?> Delete(Guid? id) => await _deleteMainArea.Execute(id) as DetailedReadMainAreaResponse;
        public async Task<IEnumerable<IResponse>?> GetAll(int skip, int take) => await _getMainAreaes.Execute(skip, take) as IEnumerable<ResumedReadMainAreaResponse>;
        public async Task<IResponse?> GetById(Guid? id) => await _getMainAreaById.Execute(id) as DetailedReadMainAreaResponse;
        public async Task<IResponse?> Update(Guid? id, IRequest request) => await _updateMainArea.Execute(id, (request as UpdateMainAreaInput)!) as DetailedReadMainAreaResponse;
    }
}