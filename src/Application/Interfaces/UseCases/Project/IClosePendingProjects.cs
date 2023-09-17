namespace Application.Interfaces.UseCases.Project
{
    public interface IClosePendingProjects
    {
        /// <summary>
        /// Encerra todos os projetos que estão com alguma pendência e cujo prazo de resolução da pendência já tenha expirado.
        /// </summary>
        /// <returns>Resultado do processo de encerramento dos projetos</returns>
        Task<string> ExecuteAsync();
    }
}