using System.Globalization;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace Services
{
    public class ReportService : IReportService
    {
        private readonly string? _outputPath;
        private readonly CultureInfo _cultureInfo;
        private readonly Dictionary<DayOfWeek, string> _dayOfWeekMap;
        private string? _certificateTemplate;
        public ReportService(IConfiguration configuration)
        {
            // Obtém cultura brasileira para formatação de datas
            _cultureInfo = new CultureInfo("pt-BR");

            // Obtém o caminho do diretório temporário
            _outputPath = configuration["TempPath"];

            // Mapeamento dos dias da semana em inglês para português
            _dayOfWeekMap = new()
            {
                { DayOfWeek.Sunday, "Domingo" },
                { DayOfWeek.Monday, "Segunda-feira" },
                { DayOfWeek.Tuesday, "Terça-feira" },
                { DayOfWeek.Wednesday, "Quarta-feira" },
                { DayOfWeek.Thursday, "Quinta-feira" },
                { DayOfWeek.Friday, "Sexta-feira" },
                { DayOfWeek.Saturday, "Sábado" }
            };
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

            // Preenche o template do certificado
            if (string.IsNullOrEmpty(_certificateTemplate))
            {
                string basePath = AppContext.BaseDirectory;

                // Obtém conteúdo dos arquivos HTML, CSS e SVG
                _certificateTemplate = await File.ReadAllTextAsync(Path.Combine(basePath, "Reports/certificate.html"));
                string bootstrapCSS = await File.ReadAllTextAsync(Path.Combine(basePath, "Reports/bootstrap.min.css"));
                string logoCefet = Convert.ToBase64String(File.ReadAllBytes(Path.Combine(basePath, "Reports/logo-cefet.svg")));
                string carimboCefet = Convert.ToBase64String(File.ReadAllBytes(Path.Combine(basePath, "Reports/carimbo.svg")));

                // Substitui as variáveis do HTML pelos valores fixos do template
                _certificateTemplate = _certificateTemplate
                    .Replace("#BOOTSTRAP_CSS#", bootstrapCSS)
                    .Replace("#LOGO_CEFET#", logoCefet)
                    .Replace("#CARIMBO_CEFET#", carimboCefet);
            }

            // Substitui as variáveis do HTML pelos valores do projeto
            string template = _certificateTemplate
                .Replace("#NOME_ORIENTADOR#", project?.Professor?.User?.Name)
                .Replace("#SUBAREA_PROJETO#", project?.SubArea?.Name)
                .Replace("#NOME_ORIENTADO#", project?.Student?.User?.Name)
                .Replace("#DATA_EDITAL#", noticeDate)
                .Replace("#PIBIC_TIPO#", $"{project?.ProgramType?.Name} / CEFET")
                .Replace("#INIP_EDITAL#", project?.Notice?.SendingDocsEndDate?.ToString("dd/MM/yyyy"))
                .Replace("#FIMP_EDITAL#", project?.Notice?.FinalReportDeadline?.ToString("dd/MM/yyyy"))
                .Replace("#SITP_EDITAL#", studentSituation)
                .Replace("#TITULO_PROJETO_ALUNO#", project?.Title)
                .Replace("#DIA_SEMANA#", _dayOfWeekMap[DateTime.Now.DayOfWeek])
                .Replace("#DIA_DATA#", DateTime.Now.Day.ToString(_cultureInfo))
                .Replace("#MES_DATA#", DateTime.Now.ToString("MMMM", _cultureInfo))
                .Replace("#ANO_DATA#", DateTime.Now.Year.ToString(_cultureInfo))
                .Replace("#NOME_COORDENADOR#", cordinatorName);

            // Transforma HTML em PDF
            await new BrowserFetcher().DownloadAsync();
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
            var page = await browser.NewPageAsync();
            await page.SetContentAsync(template);
            await page.PdfAsync(outputPath, new PdfOptions
            {
                Format = PaperFormat.A4,
                PrintBackground = true,
                MarginOptions = new MarginOptions
                {
                    Top = "20px",
                    Bottom = "20px",
                    Left = "20px",
                    Right = "20px"
                }
            });
            await browser.CloseAsync();

            // Retorna caminho onde foi salvo o arquivo
            return outputPath;
        }

        public async Task<string> GenerateCertificateTestAsync(string fileName)
        {
            // Caminho temporário onde será salvo o arquivo
            string outputPath = Path.Combine(_outputPath!, fileName);

            // Preenche o template do certificado
            if (string.IsNullOrEmpty(_certificateTemplate))
            {
                string basePath = AppContext.BaseDirectory;

                // Obtém conteúdo dos arquivos HTML, CSS e SVG
                _certificateTemplate = await File.ReadAllTextAsync(Path.Combine(basePath, "Reports/certificate.html"));
                string bootstrapCSS = await File.ReadAllTextAsync(Path.Combine(basePath, "Reports/bootstrap.min.css"));
                string logoCefet = Convert.ToBase64String(File.ReadAllBytes(Path.Combine(basePath, "Reports/logo-cefet.svg")));
                string carimboCefet = Convert.ToBase64String(File.ReadAllBytes(Path.Combine(basePath, "Reports/carimbo.svg")));

                // Substitui as variáveis do HTML pelos valores fixos do template
                _certificateTemplate = _certificateTemplate
                    .Replace("#BOOTSTRAP_CSS#", bootstrapCSS)
                    .Replace("#LOGO_CEFET#", logoCefet)
                    .Replace("#CARIMBO_CEFET#", carimboCefet);
            }

            // Substitui as variáveis do HTML pelos valores do projeto
            string template = _certificateTemplate
                .Replace("#NOME_ORIENTADOR#", "Luciana Faletti")
                .Replace("#SUBAREA_PROJETO#", "Engenharia Elétrica")
                .Replace("#NOME_ORIENTADO#", "Eduardo Paes")
                .Replace("#DATA_EDITAL#", "2023 / 2")
                .Replace("#PIBIC_TIPO#", "PIBIC / CEFET")
                .Replace("#INIP_EDITAL#", "01/01/2023")
                .Replace("#FIMP_EDITAL#", "01/03/2023")
                .Replace("#SITP_EDITAL#", "Término do Período de Bolsa")
                .Replace("#TITULO_PROJETO_ALUNO#", "Teste de Projeto")
                .Replace("#DIA_SEMANA#", _dayOfWeekMap[DateTime.Now.DayOfWeek])
                .Replace("#DIA_DATA#", DateTime.Now.Day.ToString(_cultureInfo))
                .Replace("#MES_DATA#", DateTime.Now.ToString("MMMM", _cultureInfo))
                .Replace("#ANO_DATA#", DateTime.Now.Year.ToString(_cultureInfo))
                .Replace("#NOME_COORDENADOR#", "Diego Haddad");

            // Transforma HTML em PDF
            await new BrowserFetcher().DownloadAsync();
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
            var page = await browser.NewPageAsync();
            await page.SetContentAsync(template);
            await page.PdfAsync(outputPath, new PdfOptions
            {
                Format = PaperFormat.A4,
                PrintBackground = true,
                MarginOptions = new MarginOptions
                {
                    Top = "20px",
                    Bottom = "20px",
                    Left = "20px",
                    Right = "20px"
                }
            });
            await browser.CloseAsync();

            // Retorna caminho onde foi salvo o arquivo
            return outputPath;
        }
    }
}