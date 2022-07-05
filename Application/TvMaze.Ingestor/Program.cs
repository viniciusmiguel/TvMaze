using TvMaze.Ingestor;
using TvMaze.IoC;
using MediatR;

var config = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .AddJsonFile("appsettings.json")
    .Build();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        
        services.AddLogging();
        services.AddHostedService<Worker>();
        NativeInjector.InjectServicesForDaemon(services, config["SqlConnectionString"]);
        services.AddMediatR(typeof(Program));

    }).ConfigureLogging((c, b) => b.AddConsole())
      .UseConsoleLifetime()
      .Build();

await host.RunAsync();