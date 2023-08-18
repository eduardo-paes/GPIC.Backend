using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Application.Interfaces.UseCases.Student;
using Application.Ports.Student;
using Application.Validation;
using Domain.Entities.Enums;

namespace Application.UseCases.Student
{
    public class CreateStudent : ICreateStudent
    {
        #region Global Scope
        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ICampusRepository _campusRepository;
        private readonly IEmailService _emailService;
        private readonly IHashService _hashService;
        private readonly IMapper _mapper;
        public CreateStudent(IStudentRepository studentRepository,
            IUserRepository userRepository,
            ICampusRepository campusRepository,
            ICourseRepository courseRepository,
            IEmailService emailService,
            IHashService hashService,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _userRepository = userRepository;
            _campusRepository = campusRepository;
            _courseRepository = courseRepository;
            _emailService = emailService;
            _hashService = hashService;
            _mapper = mapper;
        }
        #endregion Global Scope

        public async Task<DetailedReadStudentOutput> ExecuteAsync(CreateStudentInput input)
        {
            // Realiza o map da entidade e a validação dos campos informados
            Domain.Entities.Student? student = new(input.BirthDate,
                input.RG,
                input.IssuingAgency,
                input.DispatchDate,
                (EGender)input.Gender,
                (ERace)input.Race,
                input.HomeAddress,
                input.City,
                input.UF,
                input.CEP,
                input.PhoneDDD,
                input.Phone,
                input.CellPhoneDDD,
                input.CellPhone,
                input.CampusId,
                input.CourseId,
                input.StartYear,
                input.AssistanceTypeId,
                input.RegistrationCode);

            // Verifica se já existe um usuário com o e-mail informado
            Domain.Entities.User? user = await _userRepository.GetUserByEmailAsync(input.Email);
            UseCaseException.BusinessRuleViolation(user != null,
                "Já existe um usuário com o e-mail informado.");

            // Verifica se já existe um usuário com o CPF informado
            user = await _userRepository.GetUserByCPFAsync(input.CPF);
            UseCaseException.BusinessRuleViolation(user != null,
                "Já existe um usuário com o CPF informado.");

            // Verifica se curso informado existe
            Domain.Entities.Course? course = await _courseRepository.GetByIdAsync(input.CourseId);
            UseCaseException.BusinessRuleViolation(course == null || course.DeletedAt != null,
                "Curso informado não existe.");

            // Verifica se campus informado existe
            Domain.Entities.Campus? campus = await _campusRepository.GetByIdAsync(input.CampusId);
            UseCaseException.BusinessRuleViolation(campus == null || campus.DeletedAt != null,
                "Campus informado não existe.");

            // Verifica se a senha é nula
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(input.Password), nameof(input.Password));

            // Gera hash da senha
            input.Password = _hashService.HashPassword(input.Password!);

            // Cria usuário
            user = new Domain.Entities.User(input.Name, input.Email, input.Password, input.CPF, ERole.STUDENT);

            // Adiciona usuário no banco
            user = await _userRepository.CreateAsync(user);
            UseCaseException.BusinessRuleViolation(user == null,
                "Não foi possível criar o usuário.");

            // Adiciona estudante no banco
            student.UserId = user?.Id;
            student = await _studentRepository.CreateAsync(student);
            UseCaseException.BusinessRuleViolation(student == null,
                "Não foi possível criar o estudante.");

            // Envia e-mail de confirmação
            await _emailService.SendConfirmationEmailAsync(user?.Email, user?.Name, user?.ValidationCode);

            // Salva entidade no banco
            return _mapper.Map<DetailedReadStudentOutput>(student);
        }
    }
}