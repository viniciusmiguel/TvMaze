using TvMaze.Ingestor;
using TvMaze.IoC;
using MediatR;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        NativeInjector.InjectServicesForDaemon(services);
        services.AddMediatR(typeof(Program));

    }).UseSerilog((a,b)=> {
        b.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information);
        b.WriteTo.Console();
        b.Enrich.FromLogContext();
    })
      .UseConsoleLifetime()
      .Build();

await host.RunAsync();