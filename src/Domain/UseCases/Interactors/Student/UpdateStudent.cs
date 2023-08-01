using AutoMapper;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.Student;
using Domain.UseCases.Ports.Student;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Student
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

        public async Task<DetailedReadStudentOutput> Execute(Guid? id, UpdateStudentInput model)
        {
            // Verifica se o id foi informado
            UseCaseException.NotInformedParam(id is null, nameof(id));

            // Recupera entidade que será atualizada
            Entities.Student student = await _studentRepository.GetById(id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Entities.Student));

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
            student = await _studentRepository.Update(student);

            // Salva entidade atualizada no banco
            return _mapper.Map<DetailedReadStudentOutput>(student);
        }
    }
}