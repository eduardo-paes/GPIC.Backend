using Application.Interfaces.UseCases.Project;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using WebFunctions.Models;

namespace Infrastructure.WebFunctions
{
    public class GenerateCertificate
    {
        private readonly ILogger<GenerateCertificate> _logger;
        private readonly IGenerateCertificate _generateCertificate;
        public GenerateCertificate(ILogger<GenerateCertificate> logger, IGenerateCertificate generateCertificate)
        {
            _logger = logger;
            _generateCertificate = generateCertificate;
        }

        /// <summary>
        /// Gera os certificados de todos os projetos que estão com a data de expiração vencida.
        /// Encerra os projetos que estão com a data de expiração vencida.
        /// Suspende professores que não entregaram o relatório final.
        /// Execução diária às 01:00.
        /// </summary>
        /// <param name="timer">Informações do timer.</param>
        [Function("GenerateCertificate")]
        public async Task Run([TimerTrigger("0 * * * * *")] CustomTimerInfo timer) // 0 0 1 * * *
        {
            // Informa início da execução
            _logger.LogInformation("Geração de certificados iniciada: {StartDate}", DateTime.Now);

            // Realiza a geração dos certificados
            var result = await _generateCertificate.ExecuteAsync();

            // Informa fim da execução
            _logger.LogInformation("Geração de certificados finalizada. Resultado: {Result}", result);

            // Informa próxima execução
            if (timer is not null)
            {
                _logger.LogInformation("Próxima geração de certificados: {NextExecutionTime}", timer.ScheduleStatus?.Next);
            }
        }
    }
}
