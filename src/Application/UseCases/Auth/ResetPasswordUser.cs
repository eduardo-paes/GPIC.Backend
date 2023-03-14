using Application.DTOs.Auth;
using Application.Proxies.Auth;
using Domain.Interfaces;

namespace Application.UseCases.Auth
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

        public async Task<bool> Execute(UserResetPasswordDTO dto)
        {
            try
            {
                var entity = await _repository.GetById(dto.Id);
                if (entity == null)
                    throw new Exception("Nenhum usuário encontrato para o id informado.");

                entity.UpdatePassword(dto.Password);
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }
    }
}