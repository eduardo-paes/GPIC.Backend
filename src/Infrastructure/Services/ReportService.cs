using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using PuppeteerSharp;

namespace Services
{
    public class ReportService : IReportService
    {
        private readonly string? _currentDirectory;
        private readonly string? _outputPath;
        public ReportService(IConfiguration configuration)
        {
            _currentDirectory = Path.GetDirectoryName(typeof(ReportService).Assembly.Location);
            _outputPath = configuration["TempPath"];
        }

        public async Task<string> GenerateCertificateAsync(Project project, string cordinatorName, string fileName)
        {
            // Caminho temporário onde será salvo o arquivo
            string outputPath = Path.Combine(_outputPath!, fileName);

            // Obtém o semestre do edital
            string noticeDate;
            if (project!.Notice!.RegistrationStartDate!.Value.Month > 6)
                noticeDate = $"{project.Notice.RegistrationStartDate.Value.Year} / 2";
            else
                noticeDate = $"{project.Notice.RegistrationStartDate.Value.Year} / 1";

            // Obtém a situação do aluno (Bolsista ou Voluntário)
            var studentSituation = project.IsScholarshipCandidate ? "Término do Período de Bolsa" : "Término do Período de Voluntariado";

            // Obtém o conteúdo do arquivo HTML
            string template = await File.ReadAllTextAsync(Path.Combine(_currentDirectory!, "Reports/certificate.html"));

            // Substitui as variáveis do HTML pelos valores do projeto
            template = template
                .Replace("#NOME_ORIENTADOR#", project?.Professor?.User?.Name)
                .Replace("#SUBAREA_PROJETO#", project?.SubArea?.Name)
                .Replace("#NOME_ORIENTADO#", project?.Student?.User?.Name)
                .Replace("#DATA_EDITAL#", noticeDate)
                .Replace("#PIBIC_TIPO#", $"{project?.ProgramType?.Name} / CEFET")
                .Replace("#INIP_EDITAL#", project?.Notice?.SendingDocsEndDate?.ToString("dd/MM/yyyy"))
                .Replace("#FIMP_EDITAL#", project?.Notice?.FinalReportDeadline?.ToString("dd/MM/yyyy"))
                .Replace("#SITP_EDITAL#", studentSituation)
                .Replace("#TITULO_PROJETO_ALUNO#", project?.Title)
                .Replace("#DIA_SEMANA#", DateTime.Now.DayOfWeek.ToString())
                .Replace("#DIA_DATA#", DateTime.Now.Day.ToString())
                .Replace("#MES_DATA#", DateTime.Now.ToString("MMMM"))
                .Replace("#ANO_DATA#", DateTime.Now.Year.ToString())
                .Replace("#NOME_COORDENADOR#", cordinatorName);

            // Transforma HTML em PDF
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
            var page = await browser.NewPageAsync();
            await page.SetContentAsync(template);
            await page.PdfAsync(outputPath);
            await browser.CloseAsync();

            // Retorna caminho onde foi salvo o arquivo
            return outputPath;
        }
    }
}