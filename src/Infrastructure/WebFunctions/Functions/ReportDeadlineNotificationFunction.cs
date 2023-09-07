using Application.Interfaces.UseCases.Notice;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using WebFunctions.Models;

namespace Infrastructure.Functions.WebFunctions
{
    public class ReportDeadlineNotificationFunction
    {
        private readonly ILogger<ReportDeadlineNotificationFunction> _logger;
        private readonly IReportDeadlineNotification _reportDeadlineNotification;
        public ReportDeadlineNotificationFunction(
            ILogger<ReportDeadlineNotificationFunction> logger,
            IReportDeadlineNotification reportDeadlineNotification)
        {
            _logger = logger;
            _reportDeadlineNotification = reportDeadlineNotification;
        }

        /// <summary>
        /// Envia notificação para os professores sobre o prazo para entrega dos relatórios.
        /// Execução diária às 05:00 UTC, equivalente à 02:00 BRT.
        /// </summary>
        /// <param name="timer">Informações do timer.</param>
        [Function("ReportDeadlineNotification")]
        public async Task Run([TimerTrigger("0 0 5 * * *")] CustomTimerInfo timer)
        {
            // Informa início da execução
            _logger.LogInformation("Notificação de prazo para entrega de relatório iniciada.");

            try
            {
                // Realiza a notificação dos professores e prazo para entrega dos relatórios
                string result = await _reportDeadlineNotification.ExecuteAsync();

                // Informa fim da execução
                _logger.LogInformation("Notificação de prazo para entrega de relatório finalizada. Resultado: {Result}", result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao executar notificação de prazo para entrega de relatório.", ex);
            }

            // Informa próxima execução
            if (timer is not null)
            {
                _logger.LogInformation("Próxima notificação de prazo para entrega de relatório: {NextExecutionTime}", timer.ScheduleStatus?.Next.ToString("dd/MM/yyyy HH:mm:ss"));
            }
        }
    }
}