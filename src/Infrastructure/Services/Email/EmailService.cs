using System.Net;
using System.Net.Mail;
using Domain.Interfaces.Services;

namespace Infrastructure.Services.Email
{
    public class EmailService : IEmailService
    {
        #region Global Scope
        private readonly string? _from;
        private readonly string? _smtpServer;
        private readonly int _smtpPort;
        private readonly string? _smtpUsername;
        private readonly string? _smtpPassword;
        private readonly string? _apiDomain;

        public EmailService(string? from, string? smtpServer, int smtpPort, string? smtpUsername, string? smtpPassword, string? apiDomain)
        {
            _from = from;
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUsername = smtpUsername;
            _smtpPassword = smtpPassword;
            _apiDomain = apiDomain;
        }
        #endregion

        public async Task<bool> SendConfirmationEmail(string? email, string? name, string? token)
        {
            // Verifica se os parâmetros são nulos ou vazios
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(token))
                throw new Exception("Parâmetros inválidos. Email, nome e token são obrigatórios.");

            // Lê mensagem do template em html salvo localmente
            string? currentDirectory = Path.GetDirectoryName(typeof(EmailService).Assembly.Location);
            if (currentDirectory == null)
                throw new Exception("Não foi possível encontrar o diretório atual do projeto.");

            // Lê mensagem do template em html salvo localmente
            string template = await File.ReadAllTextAsync(Path.Combine(currentDirectory!, "Email/Templates/ConfirmEmail.html"));

            // Gera mensagem de envio
            const string subject = "Confirmação de Cadastro";
            string body = template.Replace("#USER_NAME#", name).Replace("#USER_TOKEN#", token).Replace("#API_DOMAIN#", _apiDomain);

            // Tentativa de envio de email
            try
            {
                await SendEmailAsync(email, subject, body);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível enviar o email de confirmação. {ex.Message}");
            }
        }

        public Task<bool> SendResetPasswordEmail(string? email, string? name, string? token)
        {
            throw new NotImplementedException();
        }

        #region Private Methods
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            // Verifica se os parâmetros são nulos ou vazios
            if (_from == null || _smtpServer == null || _smtpUsername == null || _smtpPassword == null)
                throw new Exception("Parâmetros de configuração de email não foram encontrados.");

            // Cria objeto de mensagem
            var mc = new MailMessage(_from, email)
            {
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            // Envia mensagem
            using var smtpClient = new SmtpClient(_smtpServer, _smtpPort);
            smtpClient.Timeout = 1000000;
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
            await smtpClient.SendMailAsync(mc);
        }
        #endregion
    }
}