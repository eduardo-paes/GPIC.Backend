namespace Services
{
    public interface IDotEnvSecrets
    {
        string GetDatabaseConnectionString();
        string GetBlobStorageConnectionString();
        string GetBlobStorageContainerName();
        string GetSmtpUserName();
        string GetSmtpUserPassword();
        string GetJwtSecret();
        string GetJwtIssuer();
        string GetJwtAudience();
        string GetJwtExpirationTime();
    }
}