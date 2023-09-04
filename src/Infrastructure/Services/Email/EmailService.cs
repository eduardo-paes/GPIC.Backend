using System.Net;
using System.Net.Mail;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace Services.Email
{
    public class EmailService : IEmailService
    {
        #region Global Scope
        private readonly string? _smtpServer;
        private readonly int _smtpPort;
        private readonly string? _smtpUsername;
        private readonly string? _smtpPassword;
        private readonly string? _currentDirectory;
        private readonly string? _siteUrl;
        private readonly string? _logoGpic;

        public EmailService(string? smtpServer, int smtpPort, string? smtpUsername, string? smtpPassword, IConfiguration configuration)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUsername = smtpUsername;
            _smtpPassword = smtpPassword;
            _currentDirectory = AppContext.BaseDirectory;
            _siteUrl = configuration.GetSection("SiteUrl").Value;
            _logoGpic = Convert.ToBase64String(File.ReadAllBytes(Path.Combine(_currentDirectory, "Email/Templates/Imgs/logo-gpic-original.svg")));
        }
        #endregion Global Scope

        public async Task SendConfirmationEmailAsync(string? email, string? name, string? token)
        {
            try
            {
                // Verifica se os parâmetros são nulos ou vazios
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(token))
                {
                    throw new Exception("Parâmetros inválidos. Email, nome e token são obrigatórios.");
                }

                // Lê mensagem do template em html salvo localmente
                string template = await File.ReadAllTextAsync(Path.Combine(_currentDirectory!, "Email/Templates/ConfirmEmail.html"));

                // Gera mensagem de envio
                const string subject = "Confirmação de Cadastro";
                string body = template
                    .Replace("#LOGO_GPIC#", _logoGpic)
                    .Replace("#USER_NAME#", name)
                    .Replace("#USER_TOKEN#", token);

                // Tentativa de envio de email
                await SendEmailAsync(email, subject, body);
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível enviar o email de confirmação de e-mail. {ex.Message}");
            }
        }

        public async Task SendResetPasswordEmailAsync(string? email, string? name, string? token)
        {
            try
            {
                // Verifica se os parâmetros são nulos ou vazios
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(token))
                {
                    throw new Exception("Parâmetros inválidos. Email, nome e token são obrigatórios.");
                }

                // Lê mensagem do template em html salvo localmente
                string template = await File.ReadAllTextAsync(Path.Combine(_currentDirectory!, "Email/Templates/ResetPassword.html"));

                // Gera mensagem de envio
                const string subject = "Recuperação de Senha";
                string body = template
                    .Replace("#LOGO_GPIC#", _logoGpic)
                    .Replace("#USER_NAME#", name)
                    .Replace("#USER_TOKEN#", token);

                // Tentativa de envio de email
                await SendEmailAsync(email, subject, body);
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível enviar o email de atualização de senha. {ex.Message}");
            }
        }

        public async Task SendNoticeEmailAsync(string? email, string? name, DateTime? registrationStartDate, DateTime? registrationEndDate, string? noticeUrl)
        {
            // Verifica se os parâmetros são nulos ou vazios
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(name) || registrationStartDate == null || registrationEndDate == null || string.IsNullOrEmpty(noticeUrl))
            {
                throw new Exception("Parâmetros inválidos. Email, nome, data de início e fim das inscrições e url do edital são obrigatórios.");
            }

            // Lê mensagem do template em html salvo localmente
            string template = await File.ReadAllTextAsync(Path.Combine(_currentDirectory!, "Email/Templates/NewEdital.html"));

            // Gera mensagem de envio
            const string subject = "Novo Edital";
            string body = template
                .Replace("#LOGO_GPIC#", _logoGpic)
                .Replace("#PROFESSOR_NAME#", name)
                .Replace("#START_DATE#", registrationStartDate.Value.ToString("dd/MM/yyyy"))
                .Replace("#END_DATE#", registrationEndDate.Value.ToString("dd/MM/yyyy"))
                .Replace("#NOTICE_URL#", noticeUrl);

            // Tentativa de envio de email
            await SendEmailAsync(email, subject, body);
        }

        public async Task SendProjectNotificationEmailAsync(string? email, string? name, string? projectTitle, string? status, string? description)
        {
            // Verifica se os parâmetros são nulos ou vazios
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(projectTitle) || string.IsNullOrEmpty(status) || string.IsNullOrEmpty(description))
            {
                throw new Exception("Parâmetros inválidos. Email, título do projeto, status e descrição são obrigatórios.");
            }

            try
            {
                // Lê mensagem do template em html salvo localmente
                string template = await File.ReadAllTextAsync(Path.Combine(_currentDirectory!, "Email/Templates/ProjectStatusChange.html"));

                // Gera mensagem de envio
                const string subject = "Alteração de Status de Projeto";
                string body = template
                    .Replace("#LOGO_GPIC#", _logoGpic)
                    .Replace("#PROFESSOR_NAME#", name)
                    .Replace("#PROJECT_TITLE#", projectTitle)
                    .Replace("#PROJECT_STATUS#", status)
                    .Replace("#PROJECT_DESCRIPTION#", description);

                // Tentativa de envio de email
                await SendEmailAsync(email, subject, body);
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível enviar o email de notificação de modificação do projeto. {ex.Message}");
            }
        }

        public async Task SendRequestStudentRegisterEmailAsync(string? email)
        {
            try
            {
                // Verifica se os parâmetros são nulos ou vazios
                if (string.IsNullOrEmpty(email))
                {
                    throw new Exception("Parâmetros inválidos. Email é obrigatório.");
                }

                // Lê mensagem do template em html salvo localmente
                string template = await File.ReadAllTextAsync(Path.Combine(_currentDirectory!, "Email/Templates/RequestStudentRegister.html"));

                // Gera mensagem de envio
                const string subject = "Solicitação de Registro";
                string body = template
                    .Replace("#LOGO_GPIC#", _logoGpic)
                    .Replace("#REGISTRATION_LINK#", _siteUrl);

                // Tentativa de envio de email
                await SendEmailAsync(email, subject, body);
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível enviar o email de solicitação de cadastro do estudante. {ex.Message}");
            }
        }

        public async Task SendNotificationOfReportDeadlineEmailAsync(string? email, string? name, string? projectTitle, string? reportType, DateTime? reportDeadline)
        {
            try
            {
                // Verifica se os parâmetros são nulos ou vazios
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(projectTitle) || string.IsNullOrEmpty(reportType) || !reportDeadline.HasValue)
                {
                    throw new Exception("Parâmetros inválidos. Email, nome, título do projeto, tipo de relatório e prazo de entrega são obrigatórios.");
                }

                // Lê mensagem do template em html salvo localmente
                string template = await File.ReadAllTextAsync(Path.Combine(_currentDirectory!, "Email/Templates/NotifyOfReportDeadline.html"));

                // Gera mensagem de envio
                const string subject = "Entrega de Relatório Próxima";
                string body = template
                    .Replace("#LOGO_GPIC#", _logoGpic)
                    .Replace("#PROFESSOR_NAME#", name)
                    .Replace("#PROJECT_TITLE#", projectTitle)
                    .Replace("#REPORT_TYPE#", reportType)
                    .Replace("#REPORT_DEADLINE#", reportDeadline.Value.ToString("dd/MM/yyyy"));

                // Tentativa de envio de email
                await SendEmailAsync(email, subject, body);
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível enviar o email de notificação de prazo de entrega de relatório. {ex.Message}");
            }
        }

        #region Private Methods
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            // Verifica se os parâmetros são nulos ou vazios
            if (_smtpServer == null || _smtpUsername == null || _smtpPassword == null)
            {
                throw new Exception("Parâmetros de configuração de email não foram encontrados.");
            }

            // Cria objeto de mensagem
            MailMessage mc = new(_smtpUsername, email)
            {
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            // Envia mensagem
            using SmtpClient smtpClient = new(_smtpServer, _smtpPort);
            smtpClient.Timeout = 1000000;
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
            await smtpClient.SendMailAsync(mc);
        }
        #endregion Private Methods
    }
}