using Domain.Contracts.Student;
using Domain.Interfaces.UseCases.Student;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Entities.Enums;

namespace Domain.UseCases.Student
{
    public class UpdateStudent : IUpdateStudent
    {
        #region Global Scope
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        public UpdateStudent(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadStudentOutput> Execute(Guid? id, UpdateStudentInput dto)
        {
            // Verifica se o id foi informado
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            // Recupera entidade que será atualizada
            var student = await _studentRepository.GetById(id);

            // Verifica se o estudante foi encontrado
            if (student == null)
                throw new Exception("Nenhum estudante encontrado para o Id informado.");

            // Verifica se a entidade foi excluída
            if (student.DeletedAt != null)
                throw new Exception("O estudante informado já foi excluído.");

            // Atualiza atributos permitidos
            student.BirthDate = dto.BirthDate;
            student.CampusId = dto.CampusId;
            student.CellPhone = dto.CellPhone;
            student.CellPhoneDDD = dto.CellPhoneDDD;
            student.CEP = dto.CEP;
            student.City = dto.City;
            student.CourseId = dto.CourseId;
            student.DispatchDate = dto.DispatchDate;
            student.HomeAddress = dto.HomeAddress;
            student.IssuingAgency = dto.IssuingAgency;
            student.Phone = dto.Phone;
            student.PhoneDDD = dto.PhoneDDD;
            student.RG = dto.RG;
            student.StartYear = dto.StartYear;
            student.UF = dto.UF;

            // Enums            
            student.Race = (ERace)dto.Race;
            student.Gender = (EGender)dto.Gender;
            student.StudentAssistanceProgram = (EStudentAssistanceProgram)dto.StudentAssistanceProgram;

            // Atualiza estudante com as informações fornecidas
            student = await _studentRepository.Update(student);

            // Salva entidade atualizada no banco
            return _mapper.Map<DetailedReadStudentOutput>(student);
        }
    }
}