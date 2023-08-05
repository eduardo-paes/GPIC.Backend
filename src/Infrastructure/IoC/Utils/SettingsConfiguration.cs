using Microsoft.Extensions.Configuration;

namespace Infrastructure.IoC.Utils
{
    public static class SettingsConfiguration
    {
        public static IConfiguration GetConfiguration()
        {
            // Caminho base para o arquivo appsettings.json
            string? basePath = Path.GetDirectoryName(typeof(SettingsConfiguration).Assembly.Location);

            // Carrega informações de ambiente (.env)
            _ = DotNetEnv.Env.Load(Path.Combine(basePath!, ".env"));

            // Retorna configurações
            return new ConfigurationBuilder()
                .SetBasePath(basePath!)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}