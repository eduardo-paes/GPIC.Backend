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

        public StudentPresenterController(ICreateStudent createStudent,
            IUpdateStudent updateStudent,
            IDeleteStudent deleteStudent,
            IGetStudents getStudents,
            IGetStudentById getStudentById, IMapper mapper)
        {
            _createStudent = createStudent;
            _updateStudent = updateStudent;
            _deleteStudent = deleteStudent;
            _getStudents = getStudents;
            _getStudentById = getStudentById;
            _mapper = mapper;
        }
        #endregion

        public async Task<IResponse?> Create(IRequest request) => _mapper.Map<DetailedReadStudentResponse>(await _createStudent.Execute((request as CreateStudentInput)!));
        public async Task<IResponse?> Delete(Guid? id) => _mapper.Map<DetailedReadStudentResponse>(await _deleteStudent.Execute(id));
        public async Task<IEnumerable<IResponse>?> GetAll(int skip, int take) => await _getStudents.Execute(skip, take) as IEnumerable<ResumedReadStudentResponse>;
        public async Task<IResponse?> GetById(Guid? id) => _mapper.Map<DetailedReadStudentResponse>(await _getStudentById.Execute(id));
        public async Task<IResponse?> Update(Guid? id, IRequest request) => _mapper.Map<DetailedReadStudentResponse>(await _updateStudent.Execute(id, (request as UpdateStudentInput)!));
    }
}