using Domain.Contracts.Student;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Validation;
using Domain.Entities.Enums;

namespace Domain.UseCases
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
        #endregion

        public async Task<DetailedReadStudentOutput> Execute(CreateStudentInput input)
        {
            // Realiza o map da entidade e a validação dos campos informados
            // var entity = _mapper.Map<Entities.Student>(input);
            var entity = new Entities.Student(input.BirthDate,
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
                input.AssistanceTypeId);

            // Verifica se já existe um usuário com o e-mail informado
            var user = await _userRepository.GetUserByEmail(input.Email);
            UseCaseException.BusinessRuleViolation(user != null,
                "Já existe um usuário com o e-mail informado.");

            // Verifica se já existe um usuário com o CPF informado
            user = await _userRepository.GetUserByCPF(input.CPF);
            UseCaseException.BusinessRuleViolation(user != null,
                "Já existe um usuário com o CPF informado.");

            // Verifica se curso informado existe
            var course = await _courseRepository.GetById(input.CourseId);
            UseCaseException.BusinessRuleViolation(course == null || course.DeletedAt != null,
                "Curso informado não existe.");

            // Verifica se campus informado existe
            var campus = await _campusRepository.GetById(input.CampusId);
            UseCaseException.BusinessRuleViolation(campus == null || campus.DeletedAt != null,
                "Campus informado não existe.");

            // Verifica se a senha é nula
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(input.Password), nameof(input.Password));

            // Gera hash da senha
            input.Password = _hashService.HashPassword(input.Password!);

            // Cria usuário
            user = new Entities.User(input.Name, input.Email, input.Password, input.CPF, Entities.Enums.ERole.STUDENT);

            // Adiciona usuário no banco
            user = await _userRepository.Create(user);
            UseCaseException.BusinessRuleViolation(user == null,
                "Não foi possível criar o usuário.");

            // Adiciona estudante no banco
            entity.UserId = user?.Id;
            entity = await _studentRepository.Create(entity);
            UseCaseException.BusinessRuleViolation(entity == null,
                "Não foi possível criar o estudante.");

            // Envia e-mail de confirmação
            await _emailService.SendConfirmationEmail(user?.Email, user?.Name, user?.ValidationCode);

            // Salva entidade no banco
            return _mapper.Map<DetailedReadStudentOutput>(entity);
        }
    }
}