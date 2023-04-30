using Domain.Contracts.Professor;
using Domain.Interfaces.UseCases.Professor;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.UseCases.Professor
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

        public async Task<DetailedReadProfessorOutput> Execute(CreateProfessorInput dto)
        {
            // Realiza o map da entidade e a validação dos campos informados
            var entity = _mapper.Map<Entities.Professor>(dto);

            // Verifica se a senha é nula
            if (string.IsNullOrEmpty(dto.Password))
                throw new Exception("Senha não informada.");

            // Verifica se já existe um usuário com o e-mail informado
            var user = await _userRepository.GetUserByEmail(dto.Email);
            if (user != null)
                throw new Exception("Já existe um usuário com o e-mail informado.");

            // Verifica se já existe um usuário com o CPF informado
            user = await _userRepository.GetUserByCPF(dto.CPF);
            if (user != null)
                throw new Exception("Já existe um usuário com o CPF informado.");

            // Gera hash da senha
            dto.Password = _hashService.HashPassword(dto.Password);

            // Cria usuário
            user = new Entities.User(dto.Name, dto.Email, dto.Password, dto.CPF, Entities.Enums.ERole.PROFESSOR);

            // Adiciona usuário no banco
            user = await _userRepository.Create(user);
            if (user == null)
                throw new Exception("Não foi possível criar o usuário.");

            // Adiciona professor no banco
            entity.UserId = user.Id;
            entity = await _professorRepository.Create(entity);
            if (entity == null)
                throw new Exception("Não foi possível criar o professor.");

            // Envia e-mail de confirmação
            await _emailService.SendConfirmationEmail(user.Email, user.Name, user.ValidationCode);

            // Salva entidade no banco
            return _mapper.Map<DetailedReadProfessorOutput>(entity);
        }
    }
}