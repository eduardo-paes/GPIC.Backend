using Adapters.Gateways.Base;
using Adapters.Gateways.Campus;
using Adapters.Interfaces;
using Domain.Contracts.Campus;
using Domain.Interfaces.UseCases;

namespace Adapters.PresenterController
{
    public class CampusPresenterController : ICampusPresenterController
    {
        #region Global Scope
        private readonly ICreateCampus _createCampus;
        private readonly IUpdateCampus _updateCampus;
        private readonly IDeleteCampus _deleteCampus;
        private readonly IGetCampuses _getCampuses;
        private readonly IGetCampusById _getCampusById;

        public CampusPresenterController(ICreateCampus createCampus,
            IUpdateCampus updateCampus,
            IDeleteCampus deleteCampus,
            IGetCampuses getCampuses,
            IGetCampusById getCampusById)
        {
            _createCampus = createCampus;
            _updateCampus = updateCampus;
            _deleteCampus = deleteCampus;
            _getCampuses = getCampuses;
            _getCampusById = getCampusById;
        }
        #endregion

        public async Task<IResponse?> Create(IRequest request) => await _createCampus.Execute((request as CreateCampusInput)!) as DetailedReadCampusResponse;
        public async Task<IResponse?> Delete(Guid? id) => await _deleteCampus.Execute(id) as DetailedReadCampusResponse;
        public async Task<IEnumerable<IResponse>?> GetAll(int skip, int take) => await _getCampuses.Execute(skip, take) as IEnumerable<ResumedReadCampusResponse>;
        public async Task<IResponse?> GetById(Guid? id) => await _getCampusById.Execute(id) as DetailedReadCampusResponse;
        public async Task<IResponse?> Update(Guid? id, IRequest request) => await _updateCampus.Execute(id, (request as UpdateCampusInput)!) as DetailedReadCampusResponse;
    }
}