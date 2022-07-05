using TvMaze.Ingestor;
using TvMaze.IoC;
using MediatR;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddLogging();
        services.AddHostedService<Worker>();
        NativeInjector.InjectServicesForDaemon(services);
        services.AddMediatR(typeof(Program));

    }).ConfigureLogging((c, b) => b.AddConsole())
      .UseConsoleLifetime()
      .Build();

await host.RunAsync();