using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.IoC.Utils
{
    public static class SettingsConfiguration
    {
        public static IConfiguration GetConfiguration(HostBuilderContext? hostContext = null)
        {
            // Caminho base para o arquivo appsettings.json
            string basePath = AppContext.BaseDirectory;

            // Carrega informações de ambiente (.env)
            DotNetEnv.Env.Load(Path.Combine(basePath, ".env"));

            // Adiciona o context do host caso exista
            if (hostContext != null)
            {
                // Retorna configurações
                return new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddConfiguration(hostContext.Configuration)
                    .AddEnvironmentVariables()
                    .Build();
            }
            else
            {
                // Retorna configurações
                return new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();
            }
        }
    }
}