using Adapters.Gateways.Base;
using Adapters.Gateways.ProgramType;
using Adapters.Interfaces;
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
        private readonly IGetProgramTypes _getProgramTypees;
        private readonly IGetProgramTypeById _getProgramTypeById;

        public ProgramTypePresenterController(ICreateProgramType createProgramType,
            IUpdateProgramType updateProgramType,
            IDeleteProgramType deleteProgramType,
            IGetProgramTypes getProgramTypees,
            IGetProgramTypeById getProgramTypeById)
        {
            _createProgramType = createProgramType;
            _updateProgramType = updateProgramType;
            _deleteProgramType = deleteProgramType;
            _getProgramTypees = getProgramTypees;
            _getProgramTypeById = getProgramTypeById;
        }
        #endregion

        public async Task<IResponse?> Create(IRequest request) => await _createProgramType.Execute((request as CreateProgramTypeInput)!) as DetailedReadProgramTypeResponse;
        public async Task<IResponse?> Delete(Guid? id) => await _deleteProgramType.Execute(id) as DetailedReadProgramTypeResponse;
        public async Task<IEnumerable<IResponse>?> GetAll(int skip, int take) => await _getProgramTypees.Execute(skip, take) as IEnumerable<ResumedReadProgramTypeResponse>;
        public async Task<IResponse?> GetById(Guid? id) => await _getProgramTypeById.Execute(id) as DetailedReadProgramTypeResponse;
        public async Task<IResponse?> Update(Guid? id, IRequest request) => await _updateProgramType.Execute(id, (request as UpdateProgramTypeInput)!) as DetailedReadProgramTypeResponse;
    }
}