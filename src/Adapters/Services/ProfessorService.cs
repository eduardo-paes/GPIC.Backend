using Adapters.DTOs.Base;
using Adapters.DTOs.Professor;
using Adapters.Proxies;
using AutoMapper;
using Domain.Contracts.Professor;
using Domain.Interfaces.UseCases;

namespace Adapters.Services
{
    public class ProfessorService : IProfessorService
    {
        #region Global Scope
        private readonly ICreateProfessor _createProfessor;
        private readonly IUpdateProfessor _updateProfessor;
        private readonly IDeleteProfessor _deleteProfessor;
        private readonly IGetProfessors _getProfessors;
        private readonly IGetProfessorById _getProfessorById;
        private readonly IMapper _mapper;

        public ProfessorService(ICreateProfessor createProfessor,
            IUpdateProfessor updateProfessor,
            IDeleteProfessor deleteProfessor,
            IGetProfessors getProfessors,
            IGetProfessorById getProfessorById,
            IMapper mapper)
        {
            _createProfessor = createProfessor;
            _updateProfessor = updateProfessor;
            _deleteProfessor = deleteProfessor;
            _getProfessors = getProfessors;
            _getProfessorById = getProfessorById;
            _mapper = mapper;
        }
        #endregion

        public async Task<ResponseDTO> Create(RequestDTO model)
        {
            var dto = model as CreateProfessorDTO;
            var input = _mapper.Map<CreateProfessorInput>(dto);
            var result = await _createProfessor.Execute(input);
            return _mapper.Map<DetailedReadProfessorDTO>(result);
        }

        public async Task<ResponseDTO> Delete(Guid? id)
        {
            var result = await _deleteProfessor.Execute(id);
            return _mapper.Map<DetailedReadProfessorDTO>(result);
        }

        public async Task<IEnumerable<ResponseDTO>> GetAll(int skip, int take)
        {
            var result = await _getProfessors.Execute(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadProfessorDTO>>(result);
        }

        public async Task<ResponseDTO> GetById(Guid? id)
        {
            var result = await _getProfessorById.Execute(id);
            return _mapper.Map<DetailedReadProfessorDTO>(result);
        }

        public async Task<ResponseDTO> Update(Guid? id, RequestDTO model)
        {
            var dto = model as UpdateProfessorDTO;
            var input = _mapper.Map<UpdateProfessorInput>(dto);
            var result = await _updateProfessor.Execute(id, input);
            return _mapper.Map<DetailedReadProfessorDTO>(result);
        }
    }
}