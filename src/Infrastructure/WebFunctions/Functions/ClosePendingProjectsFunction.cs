using Application.Interfaces.UseCases.Project;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using WebFunctions.Models;

namespace WebFunctions.Functions
{
    public class ClosePendingProjectsFunction
    {
        private readonly ILogger<ClosePendingProjectsFunction> _logger;
        private readonly IClosePendingProjects _closePendingProjects;
        public ClosePendingProjectsFunction(ILogger<ClosePendingProjectsFunction> logger, IClosePendingProjects closePendingProjects)
        {
            _logger = logger;
            _closePendingProjects = closePendingProjects;
        }

        /// <summary>
        /// Encerra todos os projetos que estão com alguma pendência e que a data de expiração já passou.
        /// Execução diária às 06:00 UTC, equivalente à 03:00 BRT.
        /// </summary>
        /// <param name="timer">Informações do timer.</param>
        [Function("ClosePendingProjects")]
        public async Task Run([TimerTrigger("0 0 6 * * *")] CustomTimerInfo timer)
        {
            // Informa início da execução
            _logger.LogInformation("Encerramento de projetos com pendência e fora do prazo iniciada.");

            try
            {
                // Realiza o encerramento dos projetos
                string result = await _closePendingProjects.ExecuteAsync();

                // Informa fim da execução
                _logger.LogInformation("Encerramento de projetos com pendência e fora do prazo finalizada. Resultado: {Result}", result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao executar encerramento de projetos com pendência e fora do prazo.", ex);
            }

            // Informa próxima execução
            if (timer is not null)
            {
                _logger.LogInformation("Próxima encerramento de projetos com pendência e fora do prazo: {NextExecutionTime}", timer.ScheduleStatus?.Next.ToString("dd/MM/yyyy HH:mm:ss"));
            }
        }
    }
}