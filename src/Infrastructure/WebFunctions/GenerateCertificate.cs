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

        [Function("GenerateCertificate")]
        public async Task Run([TimerTrigger("0 * * * * *")] TimerInfo timer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            await _generateCertificate.ExecuteAsync();

            if (timer is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {timer.ScheduleStatus?.Next}");
            }
        }
    }
}
