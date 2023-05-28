using Domain.Contracts.Auth;
using Domain.Interfaces.UseCases;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.UseCases
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
        #endregion

        public async Task<string> Execute(UserResetPasswordInput input)
        {
            // Verifica se o id do usuário é nulo
            if (input.Id == null)
                throw new ArgumentNullException(nameof(input.Id), "Id do usuário não informado.");

            // Verifica se a senha é nula
            else if (input.Password == null)
                throw new ArgumentNullException(nameof(input.Password), "Senha não informada.");

            // Verifica se o token é nulo
            else if (input.Token == null)
                throw new ArgumentNullException(nameof(input.Token), "Token não informado.");

            // Busca o usuário pelo id
            var entity = await _userRepository.GetById(input.Id)
                ?? throw new Exception("Nenhum usuário encontrato para o id informado.");

            // Verifica se o token de validação é nulo
            if (string.IsNullOrEmpty(entity.ResetPasswordToken))
                throw new Exception("Solicitação de atualização de senha não permitido.");

            // Verifica se o token de validação é igual ao token informado
            input.Password = _hashService.HashPassword(input.Password);

            // Atualiza a senha do usuário
            if (!entity.UpdatePassword(input.Password, input.Token))
                throw new Exception("Token de validação inválido.");

            // Salva as alterações
            await _userRepository.Update(entity);

            // Retorna o resultado
            return "Senha atualizada com sucesso.";
        }
    }
}