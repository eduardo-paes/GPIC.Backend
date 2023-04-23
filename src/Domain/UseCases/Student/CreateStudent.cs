using Domain.Contracts.Student;
using Domain.Interfaces.UseCases.Student;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.UseCases.Student
{
    public class CreateStudent : ICreateStudent
    {
        #region Global Scope
        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ICampusRepository _campusRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        public CreateStudent(IStudentRepository studentRepository,
            IUserRepository userRepository,
            ICampusRepository campusRepository,
            ICourseRepository courseRepository,
            IEmailService emailService,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _userRepository = userRepository;
            _campusRepository = campusRepository;
            _courseRepository = courseRepository;
            _emailService = emailService;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadStudentOutput> Execute(CreateStudentInput dto)
        {
            // Realiza o map da entidade e a validação dos campos informados
            var entity = _mapper.Map<Entities.Student>(dto);

            // Verifica se já existe um usuário com o e-mail informado
            var user = await _userRepository.GetUserByEmail(dto.Email);
            if (user != null)
                throw new Exception("Já existe um usuário com o e-mail informado.");

            // Verifica se já existe um usuário com o CPF informado
            user = await _userRepository.GetUserByCPF(dto.CPF);
            if (user != null)
                throw new Exception("Já existe um usuário com o CPF informado.");

            // Verifica se curso informado existe
            var course = await _courseRepository.GetById(dto.CourseId);
            if (course == null || course.DeletedAt != null)
                throw new Exception("Curso informado não existe.");

            // Verifica se campus informado existe
            var campus = await _campusRepository.GetById(dto.CampusId);
            if (campus == null || campus.DeletedAt != null)
                throw new Exception("Campus informado não existe.");

            // Cria usuário
            user = new Entities.User(dto.Name, dto.Email, dto.Password, dto.CPF, Entities.Enums.ERole.STUDENT);

            // Adiciona usuário no banco
            user = await _userRepository.Create(user);
            if (user == null)
                throw new Exception("Não foi possível criar o usuário.");

            // Adiciona estudante no banco
            entity.UserId = user.Id;
            entity = await _studentRepository.Create(entity);
            if (entity == null)
                throw new Exception("Não foi possível criar o estudante.");

            // Envia e-mail de confirmação
            await _emailService.SendConfirmationEmail(user.Email, user.Name, user.ValidationCode);

            // Salva entidade no banco
            return _mapper.Map<DetailedReadStudentOutput>(entity);
        }
    }
}