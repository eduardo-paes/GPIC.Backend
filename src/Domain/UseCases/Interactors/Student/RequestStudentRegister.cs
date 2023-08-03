using System.Text.RegularExpressions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.UseCases.Interfaces.Student;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Student
{
    public class RequestStudentRegister : IRequestStudentRegister
    {
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        public RequestStudentRegister(IEmailService emailService, IUserRepository userRepository)
        {
            _emailService = emailService;
            _userRepository = userRepository;
        }

        public async Task<string?> ExecuteAsync(string? email)
        {
            // Verifica se o email foi informado
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(email), "Email");

            // Verifica se o email é válido
            UseCaseException.BusinessRuleViolation(!ValidateStudentEmail(email!), "Email inválido.");

            // Verifica se o email já está cadastrado
            var user = await _userRepository.GetUserByEmail(email!);

            // Se o usuário já existe, lança uma exceção
            UseCaseException.BusinessRuleViolation(user is not null, "Email já cadastrado.");

            // Solicita o registro do usuário
            await _emailService.SendRequestStudentRegisterEmailAsync(email!);

            // Retorna resultado
            return "Solicitação de registro enviada com sucesso.";
        }

        /// <summary>
        /// Verifica se o e-mail informado é válido para o estudante.
        /// Padrões aceitos:
        ///   - nome.sobrenome@aluno.cefet-rj.br
        ///   - 12345678912@cefet-rj.br
        /// </summary>
        /// <param name="email">E-mail a ser validado</param>
        /// <returns>True se o e-mail é válido, caso contrário, False</returns>
        public bool ValidateStudentEmail(string email)
        {
            // Padrão de e-mail para estudantes
            string studentEmailPattern = @"^(?<name>[a-zA-Z0-9._-]+)@(?<domain>aluno\.cefet-rj\.br|cefet-rj\.br)$";

            // Realiza o match do e-mail com o padrão
            Match emailMatch = Regex.Match(email, studentEmailPattern);

            // Retorna se o e-mail é válido de acordo com o padrão
            return emailMatch.Success;
        }
    }
}