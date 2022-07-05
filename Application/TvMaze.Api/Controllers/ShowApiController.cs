namespace TvMaze.Api.Controllers;

[Authorize]
public class ShowApiController : ControllerBase
{
    private readonly IShowAppService _showAppService;

    public ShowApiController(INotificationHandler<DomainNotification> notifications,
                             IShowAppService showAppService,
                             IMediator mediatorHandler) : base(notifications, mediatorHandler)
    {
        _showAppService = showAppService;
	}

    [HttpGet]
    [Route("api/shows")]
    public async Task<IActionResult> Get()
    {
        return Response(await _showAppService.GetAllShowsAndActors());
    }
}

