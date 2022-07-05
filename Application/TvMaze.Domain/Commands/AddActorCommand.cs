using System;
namespace TvMaze.Domain.Commands
{
	public class AddActorCommand : Command
	{
        public string Name { get; private set; }
        public DateOnly BirthDay { get; private set; }
        public AddActorCommand(string name, DateOnly birthday)
		{
            Name = name;
            BirthDay = birthday;
		}

        public override bool IsValid()
        {
            ValidationResult = new AddActorCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

