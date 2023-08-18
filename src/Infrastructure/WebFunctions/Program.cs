using Microsoft.Extensions.Hosting;
using Infrastructure.IoC;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddInfrastructure(hostContext);
        services.AddPersistence();
        services.AddExternalServices();
        services.AddApplication();
    })
    .Build();

host.Run();
