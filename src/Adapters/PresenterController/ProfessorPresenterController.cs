using Adapters.Gateways.Base;
using Adapters.Gateways.Professor;
using Adapters.Interfaces;
using AutoMapper;
using Domain.Contracts.Professor;
using Domain.Interfaces.UseCases;

namespace Adapters.PresenterController
{
    public class ProfessorPresenterController : IProfessorPresenterController
    {
        #region Global Scope
        private readonly ICreateProfessor _createProfessor;
        private readonly IUpdateProfessor _updateProfessor;
        private readonly IDeleteProfessor _deleteProfessor;
        private readonly IGetProfessors _getProfessors;
        private readonly IGetProfessorById _getProfessorById;
        private readonly IMapper _mapper;

        public ProfessorPresenterController(ICreateProfessor createProfessor,
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

        public async Task<Response> Create(Request request)
        {
            var dto = request as CreateProfessorRequest;
            var input = _mapper.Map<CreateProfessorInput>(dto);
            var result = await _createProfessor.Execute(input);
            return _mapper.Map<DetailedReadProfessorResponse>(result);
        }

        public async Task<Response> Delete(Guid? id)
        {
            var result = await _deleteProfessor.Execute(id);
            return _mapper.Map<DetailedReadProfessorResponse>(result);
        }

        public async Task<IEnumerable<Response>> GetAll(int skip, int take)
        {
            var result = await _getProfessors.Execute(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadProfessorResponse>>(result);
        }

        public async Task<Response> GetById(Guid? id)
        {
            var result = await _getProfessorById.Execute(id);
            return _mapper.Map<DetailedReadProfessorResponse>(result);
        }

        public async Task<Response> Update(Guid? id, Request request)
        {
            var dto = request as UpdateProfessorRequest;
            var input = _mapper.Map<UpdateProfessorInput>(dto);
            var result = await _updateProfessor.Execute(id, input);
            return _mapper.Map<DetailedReadProfessorResponse>(result);
        }
    }
}