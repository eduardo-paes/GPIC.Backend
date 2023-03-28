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
        private readonly IUserRepository _repository;
        public ResetPasswordUser(IUserRepository repository)
        {
            _repository = repository;
        }
        #endregion

        public async Task<bool> Execute(UserResetPasswordInput dto)
        {
            try
            {
                var entity = await _repository.GetById(dto.Id);
                if (entity == null)
                    throw new Exception("Nenhum usuário encontrato para o id informado.");

                entity.Password = dto.Password;
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }
    }
}