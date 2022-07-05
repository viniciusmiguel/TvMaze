namespace TvMaze.Api.Controllers;

public abstract class ControllerBase : Controller
{
    private readonly DomainNotificationHandler _notifications;
    private readonly IMediator _mediatorHandler;

    protected ControllerBase(INotificationHandler<DomainNotification> notifications,
                             IMediator mediatorHandler)
    {
        _notifications = (DomainNotificationHandler)notifications;
        _mediatorHandler = mediatorHandler;
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

    protected new IActionResult Response(object result = null)
    {
        if (ValidOperation())
        {
            return Ok(new
            {
                success = true,
                data = result
            });
        }

        return BadRequest(new
        {
            success = false,
            errors = _notifications.GetNotifications().Select(n => n.Value)
        });
    }
}
