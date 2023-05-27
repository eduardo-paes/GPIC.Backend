using Adapters.Gateways.Base;
using Adapters.Gateways.Campus;
using Adapters.Interfaces;
using AutoMapper;
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
        #endregion

        public async Task<Response> Create(Request request)
        {
            var dto = request as CreateCampusRequest;
            var input = _mapper.Map<CreateCampusInput>(dto);
            var result = await _createCampus.Execute(input);
            return _mapper.Map<DetailedReadCampusResponse>(result);
        }

        public async Task<Response> Delete(Guid? id)
        {
            var result = await _deleteCampus.Execute(id);
            return _mapper.Map<DetailedReadCampusResponse>(result);
        }

        public async Task<IEnumerable<Response>> GetAll(int skip, int take)
        {
            var result = await _getCampuses.Execute(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadCampusResponse>>(result);
        }

        public async Task<Response> GetById(Guid? id)
        {
            var result = await _getCampusById.Execute(id);
            return _mapper.Map<DetailedReadCampusResponse>(result);
        }

        public async Task<Response> Update(Guid? id, Request request)
        {
            var dto = request as UpdateCampusRequest;
            var input = _mapper.Map<UpdateCampusInput>(dto);
            var result = await _updateCampus.Execute(id, input);
            return _mapper.Map<DetailedReadCampusResponse>(result);
        }
    }
}