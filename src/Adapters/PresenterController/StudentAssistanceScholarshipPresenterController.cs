using Adapters.Gateways.Base;
using Adapters.Gateways.StudentAssistanceScholarship;
using Adapters.Interfaces;
using Domain.Contracts.StudentAssistanceScholarship;
using Domain.Interfaces.UseCases;

namespace Adapters.PresenterController
{
    public class StudentAssistanceScholarshipPresenterController : IStudentAssistanceScholarshipPresenterController
    {
        #region Global Scope
        private readonly ICreateStudentAssistanceScholarship _createStudentAssistanceScholarship;
        private readonly IUpdateStudentAssistanceScholarship _updateStudentAssistanceScholarship;
        private readonly IDeleteStudentAssistanceScholarship _deleteStudentAssistanceScholarship;
        private readonly IGetStudentAssistanceScholarships _getStudentAssistanceScholarshipes;
        private readonly IGetStudentAssistanceScholarshipById _getStudentAssistanceScholarshipById;

        public StudentAssistanceScholarshipPresenterController(ICreateStudentAssistanceScholarship createStudentAssistanceScholarship,
            IUpdateStudentAssistanceScholarship updateStudentAssistanceScholarship,
            IDeleteStudentAssistanceScholarship deleteStudentAssistanceScholarship,
            IGetStudentAssistanceScholarships getStudentAssistanceScholarshipes,
            IGetStudentAssistanceScholarshipById getStudentAssistanceScholarshipById)
        {
            _createStudentAssistanceScholarship = createStudentAssistanceScholarship;
            _updateStudentAssistanceScholarship = updateStudentAssistanceScholarship;
            _deleteStudentAssistanceScholarship = deleteStudentAssistanceScholarship;
            _getStudentAssistanceScholarshipes = getStudentAssistanceScholarshipes;
            _getStudentAssistanceScholarshipById = getStudentAssistanceScholarshipById;
        }
        #endregion

        public async Task<IResponse?> Create(IRequest request) => await _createStudentAssistanceScholarship.Execute((request as CreateStudentAssistanceScholarshipInput)!) as DetailedReadStudentAssistanceScholarshipResponse;
        public async Task<IResponse?> Delete(Guid? id) => await _deleteStudentAssistanceScholarship.Execute(id) as DetailedReadStudentAssistanceScholarshipResponse;
        public async Task<IEnumerable<IResponse>?> GetAll(int skip, int take) => await _getStudentAssistanceScholarshipes.Execute(skip, take) as IEnumerable<ResumedReadStudentAssistanceScholarshipResponse>;
        public async Task<IResponse?> GetById(Guid? id) => await _getStudentAssistanceScholarshipById.Execute(id) as DetailedReadStudentAssistanceScholarshipResponse;
        public async Task<IResponse?> Update(Guid? id, IRequest request) => await _updateStudentAssistanceScholarship.Execute(id, (request as UpdateStudentAssistanceScholarshipInput)!) as DetailedReadStudentAssistanceScholarshipResponse;
    }
}