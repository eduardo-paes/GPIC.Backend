using Domain.Contracts.Auth;
using Domain.Interfaces.UseCases.Auth;
using Domain.Interfaces.Repositories;
using System.Threading.Tasks;
using System;

namespace Domain.UseCases.Auth
{
    public class ResetPasswordUser : IResetPasswordUser
    {
        #region Global Scope
        private readonly IUserRepository _userRepository;
        public ResetPasswordUser(IUserRepository userRepository) => _userRepository = userRepository;
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

            // Atualiza a senha do usuário
            entity.UpdatePassword(dto.Password, dto.Token);

            // Salva as alterações
            await _userRepository.Update(entity);

            // Retorna o resultado
            return "Senha atualizada com sucesso.";
        }
    }
}