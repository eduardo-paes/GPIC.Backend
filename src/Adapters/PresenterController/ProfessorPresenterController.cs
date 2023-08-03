using Adapters.Gateways.Base;
using Adapters.Gateways.Professor;
using Adapters.Interfaces;
using AutoMapper;
using Domain.UseCases.Interfaces.Professor;
using Domain.UseCases.Ports.Professor;

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
        #endregion Global Scope

        public async Task<IResponse> Create(IRequest request)
        {
            CreateProfessorRequest? dto = request as CreateProfessorRequest;
            CreateProfessorInput input = _mapper.Map<CreateProfessorInput>(dto);
            DetailedReadProfessorOutput result = await _createProfessor.ExecuteAsync(input);
            return _mapper.Map<DetailedReadProfessorResponse>(result);
        }

        public async Task<IResponse> Delete(Guid? id)
        {
            DetailedReadProfessorOutput result = await _deleteProfessor.ExecuteAsync(id);
            return _mapper.Map<DetailedReadProfessorResponse>(result);
        }

        public async Task<IEnumerable<IResponse>> GetAll(int skip, int take)
        {
            IQueryable<ResumedReadProfessorOutput> result = await _getProfessors.ExecuteAsync(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadProfessorResponse>>(result);
        }

        public async Task<IResponse> GetById(Guid? id)
        {
            DetailedReadProfessorOutput result = await _getProfessorById.ExecuteAsync(id);
            return _mapper.Map<DetailedReadProfessorResponse>(result);
        }

        public async Task<IResponse> Update(Guid? id, IRequest request)
        {
            UpdateProfessorRequest? dto = request as UpdateProfessorRequest;
            UpdateProfessorInput input = _mapper.Map<UpdateProfessorInput>(dto);
            DetailedReadProfessorOutput result = await _updateProfessor.ExecuteAsync(id, input);
            return _mapper.Map<DetailedReadProfessorResponse>(result);
        }
    }
}