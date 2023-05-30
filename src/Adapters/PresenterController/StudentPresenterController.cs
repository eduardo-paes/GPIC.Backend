using Adapters.Gateways.Base;
using Adapters.Gateways.Student;
using Adapters.Interfaces;
using AutoMapper;
using Domain.Contracts.Student;
using Domain.Interfaces.UseCases;

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
        private readonly IMapper _mapper;

        public StudentPresenterController(ICreateStudent createStudent, IUpdateStudent updateStudent, IDeleteStudent deleteStudent, IGetStudents getStudents, IGetStudentById getStudentById, IMapper mapper)
        {
            _createStudent = createStudent;
            _updateStudent = updateStudent;
            _deleteStudent = deleteStudent;
            _getStudents = getStudents;
            _getStudentById = getStudentById;
            _mapper = mapper;
        }
        #endregion

        public async Task<IResponse> Create(IRequest request)
        {
            var dto = request as CreateStudentRequest;
            var input = _mapper.Map<CreateStudentInput>(dto);
            var result = await _createStudent.Execute(input);
            return _mapper.Map<DetailedReadStudentResponse>(result);
        }

        public async Task<IResponse> Delete(Guid? id)
        {
            var result = await _deleteStudent.Execute(id);
            return _mapper.Map<DetailedReadStudentResponse>(result);
        }

        public async Task<IEnumerable<IResponse>> GetAll(int skip, int take)
        {
            var result = await _getStudents.Execute(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadStudentResponse>>(result);
        }

        public async Task<IResponse> GetById(Guid? id)
        {
            var result = await _getStudentById.Execute(id);
            return _mapper.Map<DetailedReadStudentResponse>(result);
        }

        public async Task<IResponse> Update(Guid? id, IRequest request)
        {
            var dto = request as UpdateStudentRequest;
            var input = _mapper.Map<UpdateStudentInput>(dto);
            var result = await _updateStudent.Execute(id, input);
            return _mapper.Map<DetailedReadStudentResponse>(result);
        }
    }
}