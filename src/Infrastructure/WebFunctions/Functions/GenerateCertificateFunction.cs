using Application.Interfaces.UseCases.Project;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using WebFunctions.Models;

namespace Infrastructure.Functions.WebFunctions
{
    public class GenerateCertificateFunction
    {
        private readonly ILogger<GenerateCertificateFunction> _logger;
        private readonly IGenerateCertificate _generateCertificate;
        public GenerateCertificateFunction(ILogger<GenerateCertificateFunction> logger, IGenerateCertificate generateCertificate)
        {
            _logger = logger;
            _generateCertificate = generateCertificate;
        }

        /// <summary>
        /// Gera os certificados de todos os projetos que estão com a data de expiração vencida.
        /// Encerra os projetos que estão com a data de expiração vencida.
        /// Suspende professores que não entregaram o relatório final.
        /// Execução diária às 04:00 UTC, equivalente à 01:00 BRT.
        /// </summary>
        /// <param name="timer">Informações do timer.</param>
        [Function("GenerateCertificate")]
        public async Task Run([TimerTrigger("0 0 4 * * *")] CustomTimerInfo timer)
        {
            // Informa início da execução
            _logger.LogInformation("Geração de certificados iniciada.");

            try
            {
                // Realiza a geração dos certificados
                string result = await _generateCertificate.ExecuteAsync();

                // Informa fim da execução
                _logger.LogInformation("Geração de certificados finalizada. Resultado: {Result}", result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao executar geração de certificados.", ex);
            }

            // Informa próxima execução
            if (timer is not null)
            {
                _logger.LogInformation("Próxima geração de certificados: {NextExecutionTime}", timer.ScheduleStatus?.Next.ToString("dd/MM/yyyy HH:mm:ss"));
            }
        }
    }
}
