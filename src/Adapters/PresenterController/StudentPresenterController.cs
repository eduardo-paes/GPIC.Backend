using Adapters.Gateways.Base;
using Adapters.Gateways.Student;
using Adapters.Interfaces;
using AutoMapper;
using Domain.UseCases.Interfaces.Student;
using Domain.UseCases.Ports.Student;

namespace Adapters.PresenterController
{
    public class StudentPresenterController : IStudentPresenterController
    {
        #region Global Scope
        private readonly ICreateStudent _createStudent;
        private readonly IUpdateStudent _updateStudent;
        private readonly IDeleteStudent _deleteStudent;
        private readonly IGetStudents _getStudents;
        private readonly IGetStudentById _getStudentById;
        private readonly IGetStudentByRegistrationCode _getStudentByRegistrationCode;
        private readonly IMapper _mapper;

        public StudentPresenterController(ICreateStudent createStudent, IUpdateStudent updateStudent, IDeleteStudent deleteStudent, IGetStudents getStudents, IGetStudentById getStudentById, IGetStudentByRegistrationCode getStudentByRegistrationCode, IMapper mapper)
        {
            _createStudent = createStudent;
            _updateStudent = updateStudent;
            _deleteStudent = deleteStudent;
            _getStudents = getStudents;
            _getStudentById = getStudentById;
            _getStudentByRegistrationCode = getStudentByRegistrationCode;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<IResponse> Create(IRequest request)
        {
            CreateStudentRequest? dto = request as CreateStudentRequest;
            CreateStudentInput input = _mapper.Map<CreateStudentInput>(dto);
            DetailedReadStudentOutput result = await _createStudent.Execute(input);
            return _mapper.Map<DetailedReadStudentResponse>(result);
        }

        public async Task<IResponse> Delete(Guid? id)
        {
            DetailedReadStudentOutput result = await _deleteStudent.Execute(id);
            return _mapper.Map<DetailedReadStudentResponse>(result);
        }

        public async Task<IEnumerable<IResponse>> GetAll(int skip, int take)
        {
            IQueryable<ResumedReadStudentOutput> result = await _getStudents.Execute(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadStudentResponse>>(result);
        }

        public async Task<IResponse> GetById(Guid? id)
        {
            DetailedReadStudentOutput result = await _getStudentById.Execute(id);
            return _mapper.Map<DetailedReadStudentResponse>(result);
        }

        public async Task<DetailedReadStudentResponse> GetByRegistrationCode(string? registrationCode)
        {
            DetailedReadStudentOutput result = await _getStudentByRegistrationCode.Execute(registrationCode);
            return _mapper.Map<DetailedReadStudentResponse>(result);
        }

        public async Task<IResponse> Update(Guid? id, IRequest request)
        {
            UpdateStudentRequest? dto = request as UpdateStudentRequest;
            UpdateStudentInput input = _mapper.Map<UpdateStudentInput>(dto);
            DetailedReadStudentOutput result = await _updateStudent.Execute(id, input);
            return _mapper.Map<DetailedReadStudentResponse>(result);
        }
    }
}