using Domain.Interfaces.Services;
using MailKit.Net.Smtp;
using MimeKit;

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

        public EmailService(string? from, string? smtpServer, int smtpPort, string? smtpUsername, string? smtpPassword)
        {
            _from = from;
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUsername = smtpUsername;
            _smtpPassword = smtpPassword;
        }
        #endregion

        public async Task<bool> SendConfirmationEmail(string? email, string? name, string? token)
        {
            // Verifica se os parâmetros são nulos ou vazios
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(token))
                throw new Exception("Parâmetros inválidos. Email, nome e token são obrigatórios.");

            // Lê mensagem do template em html salvo localmente
            var template = await File.ReadAllTextAsync("./Email/Templates/ConfirmEmail.html");

            // Gera mensagem de envio
            const string subject = "Confirmação de Cadastro";
            string body = string.Format(@template, name, token);

            // Tentativa de envio de email
            try
            {
                await SendEmailAsync(email, name, subject, body);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<bool> SendResetPasswordEmail(string? email, string? name, string? token)
        {
            throw new NotImplementedException();
        }

        #region Private Methods
        public async Task SendEmailAsync(string email, string recipientName, string subject, string message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_smtpUsername, _from));
            mimeMessage.To.Add(new MailboxAddress(email, recipientName));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart("plain") { Text = message };

            using var smtpClient = new SmtpClient();
            await smtpClient.ConnectAsync(_smtpServer, _smtpPort, useSsl: true);
            await smtpClient.AuthenticateAsync(_from, _smtpPassword);
            await smtpClient.SendAsync(mimeMessage);
            await smtpClient.DisconnectAsync(quit: true);
        }
        #endregion
    }
}