using Adapters.DTOs.Base;
using Adapters.DTOs.Student;
using Adapters.Proxies;
using AutoMapper;
using Domain.Contracts.Student;
using Domain.Interfaces.UseCases;

namespace Adapters.Services
{
    public class StudentService : IStudentService
    {
        #region Global Scope
        private readonly ICreateStudent _createStudent;
        private readonly IUpdateStudent _updateStudent;
        private readonly IDeleteStudent _deleteStudent;
        private readonly IGetStudents _getStudents;
        private readonly IGetStudentById _getStudentById;
        private readonly IMapper _mapper;

        public StudentService(ICreateStudent createStudent, IUpdateStudent updateStudent, IDeleteStudent deleteStudent, IGetStudents getStudents, IGetStudentById getStudentById, IMapper mapper)
        {
            _createStudent = createStudent;
            _updateStudent = updateStudent;
            _deleteStudent = deleteStudent;
            _getStudents = getStudents;
            _getStudentById = getStudentById;
            _mapper = mapper;
        }
        #endregion

        public async Task<ResponseDTO> Create(RequestDTO model)
        {
            var dto = model as CreateStudentDTO;
            var input = _mapper.Map<CreateStudentInput>(dto);
            var result = await _createStudent.Execute(input);
            return _mapper.Map<DetailedReadStudentDTO>(result);
        }

        public async Task<ResponseDTO> Delete(Guid? id)
        {
            var result = await _deleteStudent.Execute(id);
            return _mapper.Map<DetailedReadStudentDTO>(result);
        }

        public async Task<IEnumerable<ResponseDTO>> GetAll(int skip, int take)
        {
            var result = await _getStudents.Execute(skip, take);
            return _mapper.Map<IEnumerable<ResumedReadStudentDTO>>(result);
        }

        public async Task<ResponseDTO> GetById(Guid? id)
        {
            var result = await _getStudentById.Execute(id);
            return _mapper.Map<DetailedReadStudentDTO>(result);
        }

        public async Task<ResponseDTO> Update(Guid? id, RequestDTO model)
        {
            var dto = model as UpdateStudentDTO;
            var input = _mapper.Map<UpdateStudentInput>(dto);
            var result = await _updateStudent.Execute(id, input);
            return _mapper.Map<DetailedReadStudentDTO>(result);
        }
    }
}