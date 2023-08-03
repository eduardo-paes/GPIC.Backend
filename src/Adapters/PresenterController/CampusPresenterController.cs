using Adapters.Gateways.Base;
using Adapters.Gateways.Campus;
using Adapters.Interfaces;
using AutoMapper;
using Domain.UseCases.Interfaces.Campus;
using Domain.UseCases.Ports.Campus;

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
        private readonly IMapper _mapper;

        public CampusPresenterController(ICreateCampus createCampus, IUpdateCampus updateCampus, IDeleteCampus deleteCampus, IGetCampuses getCampuses, IGetCampusById getCampusById, IMapper mapper)
        {
            _createCampus = createCampus;
            _updateCampus = updateCampus;
            _deleteCampus = deleteCampus;
            _getCampuses = getCampuses;
            _getCampusById = getCampusById;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<IResponse> Create(IRequest request)
        {
            CreateCampusRequest? dto = request as CreateCampusRequest;
            CreateCampusInput input = _mapper.Map<CreateCampusInput>(dto);
            DetailedReadCampusOutput result = await _createCampus.ExecuteAsync(input);
            return _mapper.Map<DetailedReadCampusResponse>(result);
        }

        public async Task<IResponse> Delete(Guid? id)
        {
            DetailedReadCampusOutput result = await _deleteCampus.ExecuteAsync(id);
            return _mapper.Map<DetailedReadCampusResponse>(result);
        }

        public async Task<IEnumerable<IResponse>> GetAll(int skip, int take)
        {
            IQueryable<ResumedReadCampusOutput> result = await _getCampuses.ExecuteAsync(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadCampusResponse>>(result);
        }

        public async Task<IResponse> GetById(Guid? id)
        {
            DetailedReadCampusOutput result = await _getCampusById.ExecuteAsync(id);
            return _mapper.Map<DetailedReadCampusResponse>(result);
        }

        public async Task<IResponse> Update(Guid? id, IRequest request)
        {
            UpdateCampusRequest? dto = request as UpdateCampusRequest;
            UpdateCampusInput input = _mapper.Map<UpdateCampusInput>(dto);
            DetailedReadCampusOutput result = await _updateCampus.ExecuteAsync(id, input);
            return _mapper.Map<DetailedReadCampusResponse>(result);
        }
    }
}