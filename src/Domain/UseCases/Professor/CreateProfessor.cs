using Domain.Contracts.Professor;
using Domain.Interfaces.UseCases;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Validation;

namespace Domain.UseCases
{
    public class CreateProfessor : ICreateProfessor
    {
        #region Global Scope
        private readonly IProfessorRepository _professorRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IHashService _hashService;
        private readonly IMapper _mapper;
        public CreateProfessor(IProfessorRepository professorRepository,
            IUserRepository userRepository,
            IEmailService emailService,
            IHashService hashService,
            IMapper mapper)
        {
            _professorRepository = professorRepository;
            _userRepository = userRepository;
            _emailService = emailService;
            _hashService = hashService;
            _mapper = mapper;
        }
        #endregion

        public async Task<DetailedReadProfessorOutput> Execute(CreateProfessorInput input)
        {
            // Realiza o map da entidade e a validação dos campos informados
            var entity = new Entities.Professor(input.SIAPEEnrollment, input.IdentifyLattes);

            // Verifica se a senha é nula
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(input.Password), nameof(input.Password));

            // Verifica se já existe um usuário com o e-mail informado
            var user = await _userRepository.GetUserByEmail(input.Email);
            UseCaseException.BusinessRuleViolation(user != null, "Já existe um usuário com o e-mail informado.");

            // Verifica se já existe um usuário com o CPF informado
            user = await _userRepository.GetUserByCPF(input.CPF);
            UseCaseException.BusinessRuleViolation(user != null, "Já existe um usuário com o CPF informado.");

            // Gera hash da senha
            input.Password = _hashService.HashPassword(input.Password!);

            // Cria usuário
            user = new Entities.User(input.Name, input.Email, input.Password, input.CPF, Entities.Enums.ERole.PROFESSOR);

            // Adiciona usuário no banco
            user = await _userRepository.Create(user);
            UseCaseException.BusinessRuleViolation(user == null, "Não foi possível criar o usuário.");

            // Adiciona professor no banco
            entity.UserId = user?.Id;
            entity = await _professorRepository.Create(entity);
            UseCaseException.BusinessRuleViolation(entity == null, "Não foi possível criar o professor.");

            // Envia e-mail de confirmação
            await _emailService.SendConfirmationEmail(user?.Email, user?.Name, user?.ValidationCode);

            // Salva entidade no banco
            return _mapper.Map<DetailedReadProfessorOutput>(entity);
        }
    }
}