namespace TvMaze.Domain.CommandHandlers;

public class AddShowCommandHandler : IRequestHandler<AddShowCommand, bool>
{
    private readonly IMediator _mediator;
    private readonly IShowRepository _showRepository;
	public AddShowCommandHandler(IMediator mediator, IShowRepository showRepository) 
	{
        _mediator = mediator;
        _showRepository = showRepository;
	}

    public async Task<bool> Handle(AddShowCommand cmd, CancellationToken cancellationToken)
    {
        if (!ValidateCommand(cmd)) return false;

        var existingShow = await _showRepository.GetShowByName(cmd.Name);

        if(existingShow is object)
        {
            return false;
        }

        var show = new Show(cmd.Name);

        await _showRepository.Add(show);

        return true;
    }

    private bool ValidateCommand(Command message)
    {
        if (message.IsValid()) return true;

        foreach (var error in message.ValidationResult.Errors)
        {
            _mediator.Publish(new DomainNotification(message.MessageType, error.ErrorMessage));
        }

        return false;
    }
}