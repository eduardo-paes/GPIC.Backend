namespace Application.Interfaces.UseCases.Notice
{
    public interface IReportDeadlineNotification
    {
        /// <summary>
        /// Envia notificação para os professores de que está próximo o prazo de entrega dos relatórios.
        /// </summary>
        /// <returns>Resultado do processo de notificação</returns>
        Task<string> ExecuteAsync();
    }
}