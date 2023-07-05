using Adapters.Gateways.Base;
using Adapters.Gateways.TypeAssistance;
using Adapters.Interfaces;
using AutoMapper;
using Domain.Contracts.TypeAssistance;
using Domain.Interfaces.UseCases;

namespace Adapters.PresenterController
{
    public class TypeAssistancePresenterController : ITypeAssistancePresenterController
    {
        #region Global Scope
        private readonly ICreateTypeAssistance _createTypeAssistance;
        private readonly IUpdateTypeAssistance _updateTypeAssistance;
        private readonly IDeleteTypeAssistance _deleteTypeAssistance;
        private readonly IGetTypeAssistances _getTypeAssistances;
        private readonly IGetTypeAssistanceById _getTypeAssistanceById;
        private readonly IMapper _mapper;

        public TypeAssistancePresenterController(ICreateTypeAssistance createTypeAssistance,
            IUpdateTypeAssistance updateTypeAssistance,
            IDeleteTypeAssistance deleteTypeAssistance,
            IGetTypeAssistances getTypeAssistances,
            IGetTypeAssistanceById getTypeAssistanceById,
            IMapper mapper)
        {
            _createTypeAssistance = createTypeAssistance;
            _updateTypeAssistance = updateTypeAssistance;
            _deleteTypeAssistance = deleteTypeAssistance;
            _getTypeAssistances = getTypeAssistances;
            _getTypeAssistanceById = getTypeAssistanceById;
            _mapper = mapper;
        }
        #endregion

        public async Task<IResponse> Create(IRequest request)
        {
            var dto = request as CreateTypeAssistanceRequest;
            var input = _mapper.Map<CreateTypeAssistanceInput>(dto);
            var result = await _createTypeAssistance.Execute(input);
            return _mapper.Map<DetailedReadTypeAssistanceResponse>(result);
        }

        public async Task<IResponse> Delete(Guid? id)
        {
            var result = await _deleteTypeAssistance.Execute(id);
            return _mapper.Map<DetailedReadTypeAssistanceResponse>(result);
        }

        public async Task<IEnumerable<IResponse>> GetAll(int skip, int take)
        {
            var result = await _getTypeAssistances.Execute(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadTypeAssistanceResponse>>(result);
        }

        public async Task<IResponse> GetById(Guid? id)
        {
            var result = await _getTypeAssistanceById.Execute(id);
            return _mapper.Map<DetailedReadTypeAssistanceResponse>(result);
        }

        public async Task<IResponse> Update(Guid? id, IRequest request)
        {
            var dto = request as UpdateTypeAssistanceRequest;
            var input = _mapper.Map<UpdateTypeAssistanceInput>(dto);
            var result = await _updateTypeAssistance.Execute(id, input);
            return _mapper.Map<DetailedReadTypeAssistanceResponse>(result);
        }
    }
}