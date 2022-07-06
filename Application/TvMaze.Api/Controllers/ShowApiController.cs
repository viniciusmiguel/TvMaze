namespace TvMaze.Api.Controllers;

public class ShowApiController : ControllerBase
{
    private readonly IShowAppService _showAppService;

    public ShowApiController(INotificationHandler<DomainNotification> notifications,
                             IShowAppService showAppService,
                             IMediator mediatorHandler,
                             IUriService uriService)
        : base(notifications, mediatorHandler, uriService)
    {
        _showAppService = showAppService;
	}

    [HttpGet]
    [Route("api/shows")]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery] PaginationFilter filter)
    {
        var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        var route = Request.Path.Value;
        var total = await _showAppService.GetNumberOfShows();
        return PagedResponse<IEnumerable<ShowViewModel>>(await _showAppService.GetAllShowsAndActors(filter.PageNumber, filter.PageSize),validFilter,total,route);
    }
}

