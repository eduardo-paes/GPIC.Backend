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

        public async Task<IResponse?> Create(IRequest request) => _mapper.Map<DetailedReadProfessorResponse>(await _createProfessor.Execute((request as CreateProfessorInput)!));
        public async Task<IResponse?> Delete(Guid? id) => _mapper.Map<DetailedReadProfessorResponse>(await _deleteProfessor.Execute(id));
        public async Task<IEnumerable<IResponse>?> GetAll(int skip, int take) => await _getProfessors.Execute(skip, take) as IEnumerable<ResumedReadProfessorResponse>;
        public async Task<IResponse?> GetById(Guid? id) => _mapper.Map<DetailedReadProfessorResponse>(await _getProfessorById.Execute(id));
        public async Task<IResponse?> Update(Guid? id, IRequest request) => _mapper.Map<DetailedReadProfessorResponse>(await _updateProfessor.Execute(id, (request as UpdateProfessorInput)!));
    }
}