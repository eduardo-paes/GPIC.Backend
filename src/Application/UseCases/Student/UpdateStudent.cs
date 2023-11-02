using AutoMapper;
using Domain.Interfaces.Repositories;
using Application.Interfaces.UseCases.Student;
using Application.Ports.Student;
using Application.Validation;
using Domain.Entities.Enums;

namespace Application.UseCases.Student
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
        #endregion Global Scope

        public async Task<DetailedReadStudentOutput> ExecuteAsync(Guid? id, UpdateStudentInput model)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Recupera entidade que será atualizada
            Domain.Entities.Student student = await _studentRepository.GetByIdAsync(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Domain.Entities.Student));

            // Verifica se a entidade foi excluída
            UseCaseException.BusinessRuleViolation(student.DeletedAt != null,
                "O estudante informado já foi excluído.");

            // Atualiza atributos permitidos
            student.BirthDate = model.BirthDate;
            student.CampusId = model.CampusId;
            student.CellPhone = model.CellPhone;
            student.CellPhoneDDD = model.CellPhoneDDD;
            student.CEP = model.CEP;
            student.City = model.City;
            student.CourseId = model.CourseId;
            student.DispatchDate = model.DispatchDate;
            student.HomeAddress = model.HomeAddress;
            student.IssuingAgency = model.IssuingAgency;
            student.Phone = model.Phone;
            student.PhoneDDD = model.PhoneDDD;
            student.RG = model.RG;
            student.StartYear = model.StartYear;
            student.UF = model.UF;
            student.AssistanceTypeId = model.AssistanceTypeId;

            // Enums            
            student.Race = (ERace)model.Race;
            student.Gender = (EGender)model.Gender;

            // Atualiza estudante com as informações fornecidas
            student = await _studentRepository.UpdateAsync(student);

            // Salva entidade atualizada no banco
            return _mapper.Map<DetailedReadStudentOutput>(student);
        }
    }
}