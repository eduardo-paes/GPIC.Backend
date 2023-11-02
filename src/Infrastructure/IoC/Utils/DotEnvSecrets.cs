using Services;

namespace Infrastructure.IoC.Utils
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

        public string GetFrontEndUrl() => DotNetEnv.Env.GetString("FRONTEND_URL");
        public string GetSeqUrl() => DotNetEnv.Env.GetString("SEQ_URL");
        public string GetSeqApiKey() => DotNetEnv.Env.GetString("SEQ_API_KEY");
        public string GetBlobStorageConnectionString() => DotNetEnv.Env.GetString("AZURE_BLOB_STORAGE_CONNECTION_STRING");
        public string GetBlobStorageContainerName() => DotNetEnv.Env.GetString("AZURE_BLOB_STORAGE_CONTAINER_NAME");
        public string GetDatabaseConnectionString() => DotNetEnv.Env.GetString("POSTGRES_CONNECTION_STRING");
        public string GetSmtpUserName() => DotNetEnv.Env.GetString("SMTP_EMAIL_USERNAME");
        public string GetSmtpUserPassword() => DotNetEnv.Env.GetString("SMTP_EMAIL_PASSWORD");
        public string GetJwtSecret() => DotNetEnv.Env.GetString("JWT_SECRET_KEY");
        public string GetJwtIssuer() => DotNetEnv.Env.GetString("JWT_ISSUER");
        public string GetJwtAudience() => DotNetEnv.Env.GetString("JWT_AUDIENCE");
        public string GetJwtExpirationTime() => DotNetEnv.Env.GetString("JWT_EXPIRE_IN");
        public bool ExecuteMigration()
        {
            try
            {
                return DotNetEnv.Env.GetBool("EXECUTE_MIGRATION");
            }
            catch
            {
                return false;
            }
        }
    }
}