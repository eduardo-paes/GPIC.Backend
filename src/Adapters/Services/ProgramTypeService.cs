using Adapters.DTOs.Base;
using Adapters.DTOs.ProgramType;
using Adapters.Proxies;
using AutoMapper;
using Domain.Contracts.ProgramType;
using Domain.Interfaces.UseCases;

namespace Adapters.Services
{
    public class ProgramTypeService : IProgramTypeService
    {
        #region Global Scope
        private readonly ICreateProgramType _createProgramType;
        private readonly IUpdateProgramType _updateProgramType;
        private readonly IDeleteProgramType _deleteProgramType;
        private readonly IGetProgramTypes _getProgramTypes;
        private readonly IGetProgramTypeById _getProgramTypeById;
        private readonly IMapper _mapper;

        public ProgramTypeService(ICreateProgramType createProgramType, IUpdateProgramType updateProgramType, IDeleteProgramType deleteProgramType, IGetProgramTypes getProgramTypes, IGetProgramTypeById getProgramTypeById, IMapper mapper)
        {
            _createProgramType = createProgramType;
            _updateProgramType = updateProgramType;
            _deleteProgramType = deleteProgramType;
            _getProgramTypes = getProgramTypes;
            _getProgramTypeById = getProgramTypeById;
            _mapper = mapper;
        }
        #endregion

        public async Task<ResponseDTO> Create(RequestDTO model)
        {
            var dto = model as CreateProgramTypeDTO;
            var input = _mapper.Map<CreateProgramTypeInput>(dto);
            var result = await _createProgramType.Execute(input);
            return _mapper.Map<DetailedReadProgramTypeDTO>(result);
        }

        public async Task<ResponseDTO> Delete(Guid? id)
        {
            var result = await _deleteProgramType.Execute(id);
            return _mapper.Map<DetailedReadProgramTypeDTO>(result);
        }

        public async Task<IEnumerable<ResponseDTO>> GetAll(int skip, int take)
        {
            var result = await _getProgramTypes.Execute(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadProgramTypeDTO>>(result);
        }

        public async Task<ResponseDTO> GetById(Guid? id)
        {
            var result = await _getProgramTypeById.Execute(id);
            return _mapper.Map<DetailedReadProgramTypeDTO>(result);
        }

        public async Task<ResponseDTO> Update(Guid? id, RequestDTO model)
        {
            var dto = model as UpdateProgramTypeDTO;
            var input = _mapper.Map<UpdateProgramTypeInput>(dto);
            var result = await _updateProgramType.Execute(id, input);
            return _mapper.Map<DetailedReadProgramTypeDTO>(result);
        }
    }
}