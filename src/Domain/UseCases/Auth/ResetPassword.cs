using Domain.Contracts.Auth;
using Domain.Interfaces.UseCases.Auth;
using Domain.Interfaces.Repositories;
using System.Threading.Tasks;
using System;
using Domain.Interfaces.Services;

namespace Domain.UseCases.Auth
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

        public async Task<string> Execute(UserResetPasswordInput dto)
        {
            // Verifica se o id do usuário é nulo
            if (dto.Id == null)
                throw new ArgumentNullException(nameof(dto.Id), "Id do usuário não informado.");

            // Verifica se a senha é nula
            else if (dto.Password == null)
                throw new ArgumentNullException(nameof(dto.Password), "Senha não informada.");

            // Verifica se o token é nulo
            else if (dto.Token == null)
                throw new ArgumentNullException(nameof(dto.Token), "Token não informado.");

            // Busca o usuário pelo id
            var entity = await _userRepository.GetById(dto.Id);
            if (entity == null)
                throw new Exception("Nenhum usuário encontrato para o id informado.");

            // Verifica se o token de validação é nulo
            if (string.IsNullOrEmpty(entity.ResetPasswordToken))
                throw new Exception("Solicitação de atualização de senha não permitido.");

            // Verifica se o token de validação é igual ao token informado
            dto.Password = _hashService.HashPassword(dto.Password);

            // Atualiza a senha do usuário
            if (!entity.UpdatePassword(dto.Password, dto.Token))
                throw new Exception("Token de validação inválido.");

            // Salva as alterações
            await _userRepository.Update(entity);

            // Retorna o resultado
            return "Senha atualizada com sucesso.";
        }
    }
}