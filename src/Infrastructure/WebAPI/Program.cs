namespace Infrastructure.WebAPI;
/// <summary>
/// Classe de iniciação da WebAPI.
/// </summary>
public static class Program
{
    /// <summary>
    /// Método principal da WebAPI.
    /// </summary>
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    /// <summary>
    /// Método de criação do Host.
    /// </summary>
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
}