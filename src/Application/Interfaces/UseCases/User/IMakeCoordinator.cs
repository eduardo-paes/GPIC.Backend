namespace Application.Interfaces.UseCases.User
{
    public interface IMakeCoordinator
    {
        /// <summary>
        /// Torna o usuário um coordenador e faz com que o coordenador anterior seja desativado.
        /// </summary>
        /// <param name="userId">Id do usuário que será tornando coordenador.</param>
        /// <returns>Retorna uma mensagem de sucesso ou erro.</returns>
        Task<string> ExecuteAsync(Guid? userId);
    }
}