using Microsoft.Extensions.Configuration;

namespace Infrastructure.IoC;
public static class SettingsConfiguration
{
    public static IConfiguration GetConfiguration()
    {
        var basePath = Path.GetDirectoryName(typeof(SettingsConfiguration).Assembly.Location);
        return new ConfigurationBuilder()
            .SetBasePath(basePath!)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
    }
}