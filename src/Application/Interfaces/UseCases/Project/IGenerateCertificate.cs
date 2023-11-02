namespace Application.Interfaces.UseCases.Project
{
    public interface IGenerateCertificate
    {
        /// <summary>
        /// Gera o certificado para todos os projetos que estão para ser encerrados.
        /// </summary>
        /// <returns>Resultado do processo de geração dos certificados</returns>
        Task<string> ExecuteAsync();
    }
}