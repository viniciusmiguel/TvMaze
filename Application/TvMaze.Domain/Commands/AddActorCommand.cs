using System;
namespace TvMaze.Domain.Commands
{
	public class AddActorCommand : Command
	{
        public string Name { get; private set; }
        public DateOnly BirthDay { get; private set; }
        public Guid ShowId { get; private set; }
        public AddActorCommand(string name, DateOnly birthday, Guid showId)
		{
            Name = name;
            BirthDay = birthday;
            ShowId = showId;
		}

        public override bool IsValid()
        {
            ValidationResult = new AddActorCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

