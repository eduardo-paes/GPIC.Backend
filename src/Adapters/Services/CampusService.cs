using Adapters.DTOs.Base;
using Adapters.DTOs.Campus;
using Adapters.Proxies;
using AutoMapper;
using Domain.Contracts.Campus;
using Domain.Interfaces.UseCases;

namespace Adapters.Services
{
    public class CampusService : ICampusService
    {
        #region Global Scope
        private readonly ICreateCampus _createCampus;
        private readonly IUpdateCampus _updateCampus;
        private readonly IDeleteCampus _deleteCampus;
        private readonly IGetCampuses _getCampuses;
        private readonly IGetCampusById _getCampusById;
        private readonly IMapper _mapper;

        public CampusService(ICreateCampus createCampus, IUpdateCampus updateCampus, IDeleteCampus deleteCampus, IGetCampuses getCampuses, IGetCampusById getCampusById, IMapper mapper)
        {
            _createCampus = createCampus;
            _updateCampus = updateCampus;
            _deleteCampus = deleteCampus;
            _getCampuses = getCampuses;
            _getCampusById = getCampusById;
            _mapper = mapper;
        }
        #endregion

        public async Task<ResponseDTO> Create(RequestDTO model)
        {
            var dto = model as CreateCampusDTO;
            var input = _mapper.Map<CreateCampusInput>(dto);
            var result = await _createCampus.Execute(input);
            return _mapper.Map<DetailedReadCampusDTO>(result);
        }

        public async Task<ResponseDTO> Delete(Guid? id)
        {
            var result = await _deleteCampus.Execute(id);
            return _mapper.Map<DetailedReadCampusDTO>(result);
        }

        public async Task<IEnumerable<ResponseDTO>> GetAll(int skip, int take)
        {
            var result = await _getCampuses.Execute(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadCampusDTO>>(result);
        }

        public async Task<ResponseDTO> GetById(Guid? id)
        {
            var result = await _getCampusById.Execute(id);
            return _mapper.Map<DetailedReadCampusDTO>(result);
        }

        public async Task<ResponseDTO> Update(Guid? id, RequestDTO model)
        {
            var dto = model as UpdateCampusDTO;
            var input = _mapper.Map<UpdateCampusInput>(dto);
            var result = await _updateCampus.Execute(id, input);
            return _mapper.Map<DetailedReadCampusDTO>(result);
        }
    }
}