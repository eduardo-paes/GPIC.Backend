using Azure.Identity;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.IoC.Utils;
public static class SettingsConfiguration
{
    public static IConfiguration GetConfiguration()
    {
        // Caminho base para o arquivo appsettings.json
        var basePath = Path.GetDirectoryName(typeof(SettingsConfiguration).Assembly.Location);

        // Cria um novo builder de configurações
        var config = new ConfigurationBuilder()
            .SetBasePath(basePath!)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddUserSecrets<UserSecret>()
            .AddEnvironmentVariables()
            .Build();

#if DEBUG
        return config;
#else
        // Cria um novo builder de configurações
        var builder = new ConfigurationBuilder();

        // Adiciona configurações do Azure App Configuration
        builder.AddAzureAppConfiguration(config.GetConnectionString("AppConfig"));

        // Adiciona configurações do Azure Key Vault
        builder.AddAzureKeyVault(new Uri("https://gpickeyvault.vault.azure.net/"),
            new DefaultAzureCredential());

        // Retorna configurações
        return builder
            .SetBasePath(basePath!)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddUserSecrets<UserSecret>()
            .AddEnvironmentVariables()
            .Build();
#endif
    }
}