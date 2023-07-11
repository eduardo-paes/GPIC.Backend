using System.Collections.Immutable;
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

        // Recupera configurações do Azure Key Vault
        var kvUrl = config.GetConnectionString("KeyVaultConfig:KVUrl");
        var tenantId = config.GetConnectionString("KeyVaultConfig:TenantId");
        var clientId = config.GetConnectionString("KeyVaultConfig:ClientId");
        var clientSecretId = config.GetConnectionString("KeyVaultConfig:ClientSecretId");

        // Cria um novo cliente do Azure Key Vault
        var client = new SecretClient(
            new Uri(kvUrl), 
            new ClientSecretCredential(tenantId, clientId, clientSecretId));

        // Adiciona configurações do Azure Key Vault
        builder.AddAzureKeyVault(
            client,
            new AddAzureKeyVaultConfigurationOptions());

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