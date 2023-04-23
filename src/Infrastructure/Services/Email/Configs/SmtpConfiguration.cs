namespace Infrastructure.Services.Email.Configs;
public class SmtpConfiguration
{
    public int Port { get; set; }
    public string? Server { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
}