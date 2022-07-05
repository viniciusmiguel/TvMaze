namespace TvMaze.Api.Controllers;

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
    [AllowAnonymous]
    public async Task<IActionResult> Get()
    {
        return Response(await _showAppService.GetAllShowsAndActors());
    }
}

