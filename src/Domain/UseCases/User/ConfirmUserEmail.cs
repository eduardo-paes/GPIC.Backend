using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UseCases.User;

namespace Domain.UseCases.User
{
    public class ConfirmUserEmail : IConfirmUserEmail
    {
        private readonly IUserRepository _userRepository;

        public ConfirmUserEmail(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Execute(Guid? userId, string? token)
        {
            // Verifica se userId é nulo
            if (userId == null)
                throw new ArgumentNullException(nameof(userId), "Id do usuário não informado.");

            // Verifica se token é nulo
            else if (token == null)
                throw new ArgumentNullException(nameof(token), "Token não informado.");

            // Busca usuário pelo id
            var user = await _userRepository.GetById(userId.Value);
            if (user == null)
                throw new Exception("Usuário não encontrado.");

            // Confirma usuário
            user.ConfirmUser(token);

            // Atualiza usuário
            await _userRepository.Update(user);

            // Retorna
            return;
        }
    }
}