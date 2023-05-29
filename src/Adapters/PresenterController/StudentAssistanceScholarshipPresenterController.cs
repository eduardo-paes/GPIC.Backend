using Adapters.Gateways.Base;
using Adapters.Gateways.StudentAssistanceScholarship;
using Adapters.Interfaces;
using AutoMapper;
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
        private readonly IGetStudentAssistanceScholarships _getStudentAssistanceScholarships;
        private readonly IGetStudentAssistanceScholarshipById _getStudentAssistanceScholarshipById;
        private readonly IMapper _mapper;

        public StudentAssistanceScholarshipPresenterController(ICreateStudentAssistanceScholarship createStudentAssistanceScholarship,
            IUpdateStudentAssistanceScholarship updateStudentAssistanceScholarship,
            IDeleteStudentAssistanceScholarship deleteStudentAssistanceScholarship,
            IGetStudentAssistanceScholarships getStudentAssistanceScholarships,
            IGetStudentAssistanceScholarshipById getStudentAssistanceScholarshipById,
            IMapper mapper)
        {
            _createStudentAssistanceScholarship = createStudentAssistanceScholarship;
            _updateStudentAssistanceScholarship = updateStudentAssistanceScholarship;
            _deleteStudentAssistanceScholarship = deleteStudentAssistanceScholarship;
            _getStudentAssistanceScholarships = getStudentAssistanceScholarships;
            _getStudentAssistanceScholarshipById = getStudentAssistanceScholarshipById;
            _mapper = mapper;
        }
        #endregion

        public async Task<IResponse> Create(IRequest request)
        {
            var dto = request as CreateStudentAssistanceScholarshipRequest;
            var input = _mapper.Map<CreateStudentAssistanceScholarshipInput>(dto);
            var result = await _createStudentAssistanceScholarship.Execute(input);
            return _mapper.Map<DetailedReadStudentAssistanceScholarshipResponse>(result);
        }

        public async Task<IResponse> Delete(Guid? id)
        {
            var result = await _deleteStudentAssistanceScholarship.Execute(id);
            return _mapper.Map<DetailedReadStudentAssistanceScholarshipResponse>(result);
        }

        public async Task<IEnumerable<IResponse>> GetAll(int skip, int take)
        {
            var result = await _getStudentAssistanceScholarships.Execute(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadStudentAssistanceScholarshipResponse>>(result);
        }

        public async Task<IResponse> GetById(Guid? id)
        {
            var result = await _getStudentAssistanceScholarshipById.Execute(id);
            return _mapper.Map<DetailedReadStudentAssistanceScholarshipResponse>(result);
        }

        public async Task<IResponse> Update(Guid? id, IRequest request)
        {
            var dto = request as UpdateStudentAssistanceScholarshipRequest;
            var input = _mapper.Map<UpdateStudentAssistanceScholarshipInput>(dto);
            var result = await _updateStudentAssistanceScholarship.Execute(id, input);
            return _mapper.Map<DetailedReadStudentAssistanceScholarshipResponse>(result);
        }
    }
}