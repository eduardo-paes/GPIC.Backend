using Domain.Contracts.Student;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Entities.Enums;

namespace Domain.UseCases
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

        public async Task<DetailedReadStudentOutput> Execute(Guid? id, UpdateStudentInput input)
        {
            // Verifica se o id foi informado
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            // Recupera entidade que será atualizada
            var student = await _studentRepository.GetById(id) ?? throw new Exception("Nenhum estudante encontrado para o Id informado.");

            // Verifica se a entidade foi excluída
            if (student.DeletedAt != null)
                throw new Exception("O estudante informado já foi excluído.");

            // Atualiza atributos permitidos
            student.BirthDate = input.BirthDate;
            student.CampusId = input.CampusId;
            student.CellPhone = input.CellPhone;
            student.CellPhoneDDD = input.CellPhoneDDD;
            student.CEP = input.CEP;
            student.City = input.City;
            student.CourseId = input.CourseId;
            student.DispatchDate = input.DispatchDate;
            student.HomeAddress = input.HomeAddress;
            student.IssuingAgency = input.IssuingAgency;
            student.Phone = input.Phone;
            student.PhoneDDD = input.PhoneDDD;
            student.RG = input.RG;
            student.StartYear = input.StartYear;
            student.UF = input.UF;
            student.TypeAssistanceId = input.TypeAssistanceId;

            // Enums            
            student.Race = (ERace)input.Race;
            student.Gender = (EGender)input.Gender;

            // Atualiza estudante com as informações fornecidas
            student = await _studentRepository.Update(student);

            // Salva entidade atualizada no banco
            return _mapper.Map<DetailedReadStudentOutput>(student);
        }
    }
}