using Domain.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Interfaces.UseCases.Auth;
using Application.Ports.Auth;
using Application.Validation;

namespace Application.UseCases.Auth
{
    public class ResetPassword : IResetPassword
    {
        #region Global Scope
        private readonly IUserRepository _userRepository;
        private readonly IHashService _hashService;
        public ResetPassword(IUserRepository userRepository, IHashService hashService)
        {
            _userRepository = userRepository;
            _hashService = hashService;
        }
        #endregion Global Scope

        public async Task<string> ExecuteAsync(UserResetPasswordInput input)
        {
            // Verifica se o id do usuário é nulo
            UseCaseException.NotInformedParam(input.Id == null, nameof(input.Id));

            // Verifica se a senha é nula
            UseCaseException.NotInformedParam(input.Password == null, nameof(input.Password));

            // Verifica se o token é nulo
            UseCaseException.NotInformedParam(input.Token == null, nameof(input.Token));

            // Busca o usuário pelo id
            var entity = await _userRepository.GetByIdAsync(input.Id)
                ?? throw UseCaseException.NotFoundEntityById(nameof(Domain.Entities.User));

            // Verifica se o token de validação é nulo
            if (string.IsNullOrEmpty(entity.ResetPasswordToken))
            {
                throw UseCaseException.BusinessRuleViolation("Solicitação de atualização de senha não permitido.");
            }

            // Verifica se o token de validação é igual ao token informado
            input.Password = _hashService.HashPassword(input.Password!);

            // Atualiza a senha do usuário
            if (!entity.UpdatePassword(input.Password, input.Token!))
            {
                throw UseCaseException.BusinessRuleViolation("Token de validação inválido.");
            }

            // Salva as alterações
            _ = await _userRepository.UpdateAsync(entity);

            // Retorna o resultado
            return "Senha atualizada com sucesso.";
        }
    }
}