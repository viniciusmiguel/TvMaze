using System;

namespace TvMaze.Domain.CommandHandlers
{
	public class AddActorCommandHandler : IRequestHandler<AddActorCommand, bool>
	{
		public AddActorCommandHandler()
		{

		}

        public async Task<bool> Handle(AddActorCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

