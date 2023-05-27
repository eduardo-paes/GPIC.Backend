using Adapters.Gateways.Base;
using Adapters.Gateways.ProgramType;
using Adapters.Interfaces;
using AutoMapper;
using Domain.Contracts.ProgramType;
using Domain.Interfaces.UseCases;

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
        #endregion

        public async Task<Response> Create(Request request)
        {
            var dto = request as CreateProgramTypeRequest;
            var input = _mapper.Map<CreateProgramTypeInput>(dto);
            var result = await _createProgramType.Execute(input);
            return _mapper.Map<DetailedReadProgramTypeResponse>(result);
        }

        public async Task<Response> Delete(Guid? id)
        {
            var result = await _deleteProgramType.Execute(id);
            return _mapper.Map<DetailedReadProgramTypeResponse>(result);
        }

        public async Task<IEnumerable<Response>> GetAll(int skip, int take)
        {
            var result = await _getProgramTypes.Execute(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadProgramTypeResponse>>(result);
        }

        public async Task<Response> GetById(Guid? id)
        {
            var result = await _getProgramTypeById.Execute(id);
            return _mapper.Map<DetailedReadProgramTypeResponse>(result);
        }

        public async Task<Response> Update(Guid? id, Request request)
        {
            var dto = request as UpdateProgramTypeRequest;
            var input = _mapper.Map<UpdateProgramTypeInput>(dto);
            var result = await _updateProgramType.Execute(id, input);
            return _mapper.Map<DetailedReadProgramTypeResponse>(result);
        }
    }
}