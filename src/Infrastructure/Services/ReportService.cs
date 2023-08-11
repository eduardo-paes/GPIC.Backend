using Domain.Entities;
using Domain.Interfaces.Services;
using PuppeteerSharp;

namespace Services
{
    public class ReportService : IReportService
    {
        private readonly string? _currentDirectory;
        public ReportService()
        {
            _currentDirectory = Path.GetDirectoryName(typeof(ReportService).Assembly.Location);
        }

        public async Task<string> GenerateCertificateAsync(Project project, string cordinatorName)
        {
            // Caminho temporário onde será salvo o arquivo
            string outputPath = "";

            // Obtém o conteúdo do arquivo HTML
            string template = await File.ReadAllTextAsync(Path.Combine(_currentDirectory!, "Reports/certificate.html"));

            // Substitui as variáveis do HTML pelos valores do projeto
            template = template
                .Replace("#NOME_ORIENTADOR#", project?.Professor?.User?.Name)
                .Replace("#NOME_DEPARTAMENTO#", "") // TODO: Inserir departamento do professor no Projeto?
                .Replace("#NOME_ORIENTADO#", project?.Student?.User?.Name)
                .Replace("#DATA_EDITAL#", "") // TODO: Verificar como obter a data do edital (Ex.: 2017 / 2)
                .Replace("#PIBIC_TIPO#", $"{project?.ProgramType?.Name} / CEFET")
                .Replace("#INIP_EDITAL#", project?.Notice?.SendingDocsEndDate?.ToString("dd/MM/yyyy")) // TODO: Validar essa informação
                .Replace("#FIMP_EDITAL#", project?.Notice?.FinalReportDeadline?.ToString("dd/MM/yyyy")) // TODO: Validar essa informação
                .Replace("#SITP_EDITAL#", "") // TODO: Pegar essa informação do Projeto?
                .Replace("#TITULO_PROJETO_ALUNO#", project?.Title)
                .Replace("#TITULO_PROJETO_ORIENTADOR#", "") // TODO: Verificar se esse campo será inserido no Projeto
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