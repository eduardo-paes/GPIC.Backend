using Adapters.Gateways.Base;
using Adapters.Gateways.SubArea;
using Adapters.Interfaces;
using AutoMapper;
using Domain.Contracts.SubArea;
using Domain.Interfaces.UseCases;

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

        public SubAreaPresenterController(ICreateSubArea createSubArea,
            IUpdateSubArea updateSubArea,
            IDeleteSubArea deleteSubArea,
            IGetSubAreasByArea getSubAreasByArea,
            IGetSubAreaById getSubAreaById,
            IMapper mapper)
        {
            _createSubArea = createSubArea;
            _updateSubArea = updateSubArea;
            _deleteSubArea = deleteSubArea;
            _getSubAreasByArea = getSubAreasByArea;
            _getSubAreaById = getSubAreaById;
            _mapper = mapper;
        }
        #endregion

        public async Task<IResponse?> Create(IRequest request) => _mapper.Map<DetailedReadSubAreaResponse>(await _createSubArea.Execute((request as CreateSubAreaInput)!));
        public async Task<IResponse?> Delete(Guid? id) => _mapper.Map<DetailedReadSubAreaResponse>(await _deleteSubArea.Execute(id));
        public Task<IEnumerable<IResponse>?> GetAll(int skip, int take) => throw new NotImplementedException();
        public async Task<IResponse?> GetById(Guid? id) => _mapper.Map<DetailedReadSubAreaResponse>(await _getSubAreaById.Execute(id));
        public async Task<IQueryable<IResponse>?> GetSubAreasByArea(Guid? areaId, int skip, int take) => await _getSubAreasByArea.Execute(areaId, skip, take) as IQueryable<ResumedReadSubAreaResponse>;
        public async Task<IResponse?> Update(Guid? id, IRequest request) => _mapper.Map<DetailedReadSubAreaResponse>(await _updateSubArea.Execute(id, (request as UpdateSubAreaInput)!));
    }
}