using AutoMapper;
using Domain.Entities.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.UseCases.Interfaces.Student;
using Domain.UseCases.Ports.Student;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Student
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

        public async Task<DetailedReadStudentOutput> ExecuteAsync(CreateStudentInput model)
        {
            // Realiza o map da entidade e a validação dos campos informados
            // var entity = _mapper.Map<Entities.Student>(input);
            Entities.Student? entity = new(model.BirthDate,
                model.RG,
                model.IssuingAgency,
                model.DispatchDate,
                (EGender)model.Gender,
                (ERace)model.Race,
                model.HomeAddress,
                model.City,
                model.UF,
                model.CEP,
                model.PhoneDDD,
                model.Phone,
                model.CellPhoneDDD,
                model.CellPhone,
                model.CampusId,
                model.CourseId,
                model.StartYear,
                model.AssistanceTypeId,
                model.RegistrationCode);

            // Verifica se já existe um usuário com o e-mail informado
            Entities.User? user = await _userRepository.GetUserByEmailAsync(model.Email);
            UseCaseException.BusinessRuleViolation(user != null,
                "Já existe um usuário com o e-mail informado.");

            // Verifica se já existe um usuário com o CPF informado
            user = await _userRepository.GetUserByCPFAsync(model.CPF);
            UseCaseException.BusinessRuleViolation(user != null,
                "Já existe um usuário com o CPF informado.");

            // Verifica se curso informado existe
            Entities.Course? course = await _courseRepository.GetByIdAsync(model.CourseId);
            UseCaseException.BusinessRuleViolation(course == null || course.DeletedAt != null,
                "Curso informado não existe.");

            // Verifica se campus informado existe
            Entities.Campus? campus = await _campusRepository.GetByIdAsync(model.CampusId);
            UseCaseException.BusinessRuleViolation(campus == null || campus.DeletedAt != null,
                "Campus informado não existe.");

            // Verifica se a senha é nula
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(model.Password), nameof(model.Password));

            // Gera hash da senha
            model.Password = _hashService.HashPassword(model.Password!);

            // Cria usuário
            user = new Entities.User(model.Name, model.Email, model.Password, model.CPF, ERole.STUDENT);

            // Adiciona usuário no banco
            user = await _userRepository.CreateAsync(user);
            UseCaseException.BusinessRuleViolation(user == null,
                "Não foi possível criar o usuário.");

            // Adiciona estudante no banco
            entity.UserId = user?.Id;
            entity = await _studentRepository.CreateAsync(entity);
            UseCaseException.BusinessRuleViolation(entity == null,
                "Não foi possível criar o estudante.");

            // Envia e-mail de confirmação
            await _emailService.SendConfirmationEmailAsync(user?.Email, user?.Name, user?.ValidationCode);

            // Salva entidade no banco
            return _mapper.Map<DetailedReadStudentOutput>(entity);
        }
    }
}