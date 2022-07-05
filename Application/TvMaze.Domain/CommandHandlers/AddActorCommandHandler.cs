using System;

namespace TvMaze.Domain.CommandHandlers
{
	public class AddActorCommandHandler : IRequestHandler<AddActorCommand, bool>
	{
        private readonly IMediator _mediator;
        private readonly IShowRepository _showRepository;
        public AddActorCommandHandler(IMediator mediator, IShowRepository showRepository)
		{
            _mediator = mediator;
            _showRepository = showRepository;
        }

        public async Task<bool> Handle(AddActorCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request)) return false;

            var existingActor = await _showRepository.GetActorByShowIdAndActorName(request.ShowId, request.Name);

            if(existingActor is object)
            {
                return false;
            }

            var actor = new Actor(request.Name, request.BirthDay, request.ShowId);

            await _showRepository.AddActor(actor);

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
}

