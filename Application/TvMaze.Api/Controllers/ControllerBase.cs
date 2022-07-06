namespace TvMaze.Api.Controllers;

public abstract class ControllerBase : Controller
{
    private readonly DomainNotificationHandler _notifications;
    private readonly IMediator _mediatorHandler;
    private readonly IUriService _uriService;
    protected ControllerBase(INotificationHandler<DomainNotification> notifications,
                             IMediator mediatorHandler,
                             IUriService uriService)
    {
        _notifications = (DomainNotificationHandler)notifications;
        _mediatorHandler = mediatorHandler;
        _uriService = uriService;
    }

    protected bool ValidOperation()
    {
        return !_notifications.HasNotifications();
    }

    protected IEnumerable<string> GetErrorMessages()
    {
        return _notifications.GetNotifications().Select(c => c.Value).ToList();
    }

    protected void NofityError(string key, string message)
    {
        _mediatorHandler.Publish(new DomainNotification(key, message));
    }

    protected new IActionResult Response<T>(T result)
    {
        if (ValidOperation())
        {
            return Ok(new Response<T>(result)
            {
                Succeeded = true
            });
        }

        return BadRequest(new Response<T>(result)
        {
            Succeeded = false,
            Errors = _notifications.GetNotifications().Select(n => n.Value)
        });
    }
    protected IActionResult PagedResponse<T>(T result, PaginationFilter filter, int totalRecords, string route)
    {
        if (ValidOperation())
        {
            return Ok(PaginationHelper.CreatePagedReponse<T>(result, filter, totalRecords, _uriService, route));
        }

        return BadRequest(new PagedResponse<T>(result, filter.PageNumber, filter.PageSize)
        {
            Succeeded = false,
            Errors = _notifications.GetNotifications().Select(n => n.Value)
        });
    }
}
