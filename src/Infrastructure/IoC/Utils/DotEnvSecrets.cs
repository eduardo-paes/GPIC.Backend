using Services;

namespace IoC.Utils
{
    public class DotEnvSecrets : IDotEnvSecrets
    {
        public DotEnvSecrets()
        {
            // Caminho base para o arquivo appsettings.json
            string? basePath = Path.GetDirectoryName(typeof(DotEnvSecrets).Assembly.Location);

            // Carrega informações de ambiente (.env)
            _ = DotNetEnv.Env.Load(Path.Combine(basePath!, ".env"));
        }

        public string GetBlobStorageConnectionString()
        {
            return DotNetEnv.Env.GetString("AZURE_BLOB_STORAGE_CONNECTION_STRING");
        }

        public string GetBlobStorageContainerName()
        {
            return DotNetEnv.Env.GetString("AZURE_BLOB_STORAGE_CONTAINER_NAME");
        }

        public string GetDatabaseConnectionString()
        {
            return DotNetEnv.Env.GetString("AZURE_POSTGRES_CONNECTION_STRING");
        }

        public string GetSmtpUserName()
        {
            return DotNetEnv.Env.GetString("SMTP_EMAIL_USERNAME");
        }

        public string GetSmtpUserPassword()
        {
            return DotNetEnv.Env.GetString("SMTP_EMAIL_PASSWORD");
        }

        public string GetJwtSecret()
        {
            return DotNetEnv.Env.GetString("JWT_SECRET_KEY");
        }

        public string GetJwtIssuer()
        {
            return DotNetEnv.Env.GetString("JWT_ISSUER");
        }

        public string GetJwtAudience()
        {
            return DotNetEnv.Env.GetString("JWT_AUDIENCE");
        }

        public string GetJwtExpirationTime()
        {
            return DotNetEnv.Env.GetString("JWT_EXPIRE_IN");
        }
    }
}