using Adapters.Gateways.AssistanceType;
using Adapters.Gateways.Base;
using Adapters.Interfaces;
using AutoMapper;
using Domain.UseCases.Interfaces.AssistanceType;
using Domain.UseCases.Ports.AssistanceType;

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
        #endregion Global Scope

        public async Task<IResponse> Create(IRequest request)
        {
            CreateAssistanceTypeRequest? dto = request as CreateAssistanceTypeRequest;
            CreateAssistanceTypeInput input = _mapper.Map<CreateAssistanceTypeInput>(dto);
            DetailedReadAssistanceTypeOutput result = await _createAssistanceType.Execute(input);
            return _mapper.Map<DetailedReadAssistanceTypeResponse>(result);
        }

        public async Task<IResponse> Delete(Guid? id)
        {
            DetailedReadAssistanceTypeOutput result = await _deleteAssistanceType.Execute(id);
            return _mapper.Map<DetailedReadAssistanceTypeResponse>(result);
        }

        public async Task<IEnumerable<IResponse>> GetAll(int skip, int take)
        {
            IQueryable<ResumedReadAssistanceTypeOutput> result = await _getAssistanceTypes.Execute(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadAssistanceTypeResponse>>(result);
        }

        public async Task<IResponse> GetById(Guid? id)
        {
            DetailedReadAssistanceTypeOutput result = await _getAssistanceTypeById.Execute(id);
            return _mapper.Map<DetailedReadAssistanceTypeResponse>(result);
        }

        public async Task<IResponse> Update(Guid? id, IRequest request)
        {
            UpdateAssistanceTypeRequest? dto = request as UpdateAssistanceTypeRequest;
            UpdateAssistanceTypeInput input = _mapper.Map<UpdateAssistanceTypeInput>(dto);
            DetailedReadAssistanceTypeOutput result = await _updateAssistanceType.Execute(id, input);
            return _mapper.Map<DetailedReadAssistanceTypeResponse>(result);
        }
    }
}