namespace Infrastructure.IoC.Utils;
public class UserSecret
{
    public SmtpConfiguration? Smtp { get; set; }
    public JwtConfiguration? Jwt { get; set; }
    public StorageFileConfiguration? StorageFile { get; set; }
    public ConnectionStringConfiguration? ConnectionString { get; set; }

    public class SmtpConfiguration
    {
        public string? Host { get; set; }
        public int Port { get; set; }
        public string? User { get; set; }
        public string? Password { get; set; }
    }

    public class JwtConfiguration
    {
        public string? Secret { get; set; }
        public int Expiration { get; set; }
    }

    public class StorageFileConfiguration
    {
        public string? ContainerName { get; set; }
        public string? ConnectionString { get; set; }
    }

    public class ConnectionStringConfiguration
    {
        public string? DefaultConnection { get; set; }
        public string? AppConfig { get; set; }
    }
}