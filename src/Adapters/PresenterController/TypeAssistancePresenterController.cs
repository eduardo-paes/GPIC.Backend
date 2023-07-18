using Adapters.Gateways.Base;
using Adapters.Gateways.AssistanceType;
using Adapters.Interfaces;
using AutoMapper;
using Domain.Contracts.AssistanceType;
using Domain.Interfaces.UseCases;

namespace Adapters.PresenterController
{
    public class AssistanceTypePresenterController : IAssistanceTypePresenterController
    {
        #region Global Scope
        private readonly ICreateAssistanceType _createAssistanceType;
        private readonly IUpdateAssistanceType _updateAssistanceType;
        private readonly IDeleteAssistanceType _deleteAssistanceType;
        private readonly IGetAssistanceTypes _getAssistanceTypes;
        private readonly IGetAssistanceTypeById _getAssistanceTypeById;
        private readonly IMapper _mapper;

        public AssistanceTypePresenterController(ICreateAssistanceType createAssistanceType,
            IUpdateAssistanceType updateAssistanceType,
            IDeleteAssistanceType deleteAssistanceType,
            IGetAssistanceTypes getAssistanceTypes,
            IGetAssistanceTypeById getAssistanceTypeById,
            IMapper mapper)
        {
            _createAssistanceType = createAssistanceType;
            _updateAssistanceType = updateAssistanceType;
            _deleteAssistanceType = deleteAssistanceType;
            _getAssistanceTypes = getAssistanceTypes;
            _getAssistanceTypeById = getAssistanceTypeById;
            _mapper = mapper;
        }
        #endregion

        public async Task<IResponse> Create(IRequest request)
        {
            var dto = request as CreateAssistanceTypeRequest;
            var input = _mapper.Map<CreateAssistanceTypeInput>(dto);
            var result = await _createAssistanceType.Execute(input);
            return _mapper.Map<DetailedReadAssistanceTypeResponse>(result);
        }

        public async Task<IResponse> Delete(Guid? id)
        {
            var result = await _deleteAssistanceType.Execute(id);
            return _mapper.Map<DetailedReadAssistanceTypeResponse>(result);
        }

        public async Task<IEnumerable<IResponse>> GetAll(int skip, int take)
        {
            var result = await _getAssistanceTypes.Execute(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadAssistanceTypeResponse>>(result);
        }

        public async Task<IResponse> GetById(Guid? id)
        {
            var result = await _getAssistanceTypeById.Execute(id);
            return _mapper.Map<DetailedReadAssistanceTypeResponse>(result);
        }

        public async Task<IResponse> Update(Guid? id, IRequest request)
        {
            var dto = request as UpdateAssistanceTypeRequest;
            var input = _mapper.Map<UpdateAssistanceTypeInput>(dto);
            var result = await _updateAssistanceType.Execute(id, input);
            return _mapper.Map<DetailedReadAssistanceTypeResponse>(result);
        }
    }
}