using TvMaze.Application;
using TvMaze.Core.Extensions;
using Newtonsoft.Json;
using System.Diagnostics;
using TvMaze.Core;
using MediatR;
using TvMaze.Core.Messages.DomainMessages;

namespace TvMaze.Ingestor;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IShowAppService _showAppService;
    private readonly string _showsUri;
    private readonly HttpClient _httpClient;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly MessageHandler _messageHandler;
    private readonly DomainNotificationHandler _notifications;
    public Worker(ILogger<Worker> logger, ISettingsReader settingsReader, IShowAppService showAppService,
        INotificationHandler<DomainNotification> notifications)
    {
        _logger = logger;
        _showAppService = showAppService;
        _showsUri = settingsReader.GetSetting("ApiShowsUri");
        _messageHandler = new MessageHandler(_logger);
        _httpClient = new HttpClient(_messageHandler);
        _notifications = (DomainNotificationHandler) notifications;
        _cancellationTokenSource = new CancellationTokenSource();
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Service stopping requested at: {time}", DateTimeOffset.Now);
        _cancellationTokenSource.Cancel();
        return base.StopAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            _logger.LogInformation("Shows Ingester Starting at: {time}", DateTimeOffset.Now);

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var shows = await GetShowsAsync(cancellationToken);

            await shows.ParallelForEachAsync(async (show) => {

                var showDomainId = await _showAppService.LocateOrCreateShow(show.name, cancellationToken);

                var actors = await GetActorsByShowId(show.id, cancellationToken);

                if (actors is null) return;

                foreach(var actor in actors)
                {
                    await _showAppService.IncludeActorIfNotExists(showDomainId, actor.person.name,DateOnly.Parse(actor.person.birthday), cancellationToken);
                }

                if(_notifications.HasNotifications())
                {
                    _logger.LogWarning("Domain Notifications generated while processing show: " + show.name);
                    foreach (var n in _notifications.GetNotifications())
                        _logger.LogWarning(n.Key + " : " + n.Value);
                }

            }, Environment.ProcessorCount);

            stopWatch.Stop();

            _logger.LogInformation("Shows Ingester Finished: {time}",stopWatch.Elapsed);

            await Task.Delay(TimeSpan.FromMinutes(10), cancellationToken);
        }
    }

    private async Task<IEnumerable<ActorDto>?> GetActorsByShowId(int showId, CancellationToken cancellationToken)
    {
        var json = await GetJsonFromUri(_showsUri + "/" + showId + "/cast", cancellationToken);
        return JsonConvert.DeserializeObject<IEnumerable<ActorDto>>(json);
    }

    private async Task<IEnumerable<ShowDto>?> GetShowsAsync(CancellationToken cancellationToken)
    {
        var json = await GetJsonFromUri(_showsUri, cancellationToken);
        return JsonConvert.DeserializeObject<IEnumerable<ShowDto>>(json);
    }

    private async Task<string> GetJsonFromUri(string uri, CancellationToken cancellationToken)
    {
        return await _httpClient.GetStringAsync(uri, cancellationToken);
    }

    class MessageHandler : HttpClientHandler
    {
        private readonly ILogger<Worker> _logger;
        public MessageHandler(ILogger<Worker> logger)
        {
            _logger = logger;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage? response = new HttpResponseMessage();
            do
            {
                try
                {
                    response = await base.SendAsync(request, cancellationToken);
                    return response;
                }
                catch (HttpRequestException)
                {
                    await Task.Delay(10000);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Unhandled exception during SendAsync: " + ex.Message);
                    await Task.Delay(10000);
                }
                await Task.Delay(1000);

            } while (!response.IsSuccessStatusCode);

            return null;          
        }
    }
}