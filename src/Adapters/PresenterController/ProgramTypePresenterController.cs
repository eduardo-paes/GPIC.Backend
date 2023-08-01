using Adapters.Gateways.Base;
using Adapters.Gateways.ProgramType;
using Adapters.Interfaces;
using AutoMapper;
using Domain.UseCases.Interfaces.ProgramType;
using Domain.UseCases.Ports.ProgramType;

namespace Adapters.PresenterController
{
    public class ProgramTypePresenterController : IProgramTypePresenterController
    {
        #region Global Scope
        private readonly ICreateProgramType _createProgramType;
        private readonly IUpdateProgramType _updateProgramType;
        private readonly IDeleteProgramType _deleteProgramType;
        private readonly IGetProgramTypes _getProgramTypes;
        private readonly IGetProgramTypeById _getProgramTypeById;
        private readonly IMapper _mapper;

        public ProgramTypePresenterController(ICreateProgramType createProgramType, IUpdateProgramType updateProgramType, IDeleteProgramType deleteProgramType, IGetProgramTypes getProgramTypes, IGetProgramTypeById getProgramTypeById, IMapper mapper)
        {
            _createProgramType = createProgramType;
            _updateProgramType = updateProgramType;
            _deleteProgramType = deleteProgramType;
            _getProgramTypes = getProgramTypes;
            _getProgramTypeById = getProgramTypeById;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<IResponse> Create(IRequest request)
        {
            CreateProgramTypeRequest? dto = request as CreateProgramTypeRequest;
            CreateProgramTypeInput input = _mapper.Map<CreateProgramTypeInput>(dto);
            DetailedReadProgramTypeOutput result = await _createProgramType.Execute(input);
            return _mapper.Map<DetailedReadProgramTypeResponse>(result);
        }

        public async Task<IResponse> Delete(Guid? id)
        {
            DetailedReadProgramTypeOutput result = await _deleteProgramType.Execute(id);
            return _mapper.Map<DetailedReadProgramTypeResponse>(result);
        }

        public async Task<IEnumerable<IResponse>> GetAll(int skip, int take)
        {
            IQueryable<ResumedReadProgramTypeOutput> result = await _getProgramTypes.Execute(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadProgramTypeResponse>>(result);
        }

        public async Task<IResponse> GetById(Guid? id)
        {
            DetailedReadProgramTypeOutput result = await _getProgramTypeById.Execute(id);
            return _mapper.Map<DetailedReadProgramTypeResponse>(result);
        }

        public async Task<IResponse> Update(Guid? id, IRequest request)
        {
            UpdateProgramTypeRequest? dto = request as UpdateProgramTypeRequest;
            UpdateProgramTypeInput input = _mapper.Map<UpdateProgramTypeInput>(dto);
            DetailedReadProgramTypeOutput result = await _updateProgramType.Execute(id, input);
            return _mapper.Map<DetailedReadProgramTypeResponse>(result);
        }
    }
}