using Microsoft.Extensions.Hosting;
using Infrastructure.IoC;
using Microsoft.Extensions.Configuration;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration? configuration = null;
        services.AddInfrastructure(ref configuration, hostContext);
        services.AddPersistence();
        services.AddExternalServices();
        services.AddApplication();
    })
    .Build();

host.Run();
