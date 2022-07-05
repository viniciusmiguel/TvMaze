using System;
namespace TvMaze.Domain.Validations
{
	public class AddActorCommandValidation : AbstractValidator<AddActorCommand>
	{
		public static string NameIsEmptyMsg = "The Actor name cannot be empty";
		public static string BirthdayIsEmptyMsg = "The Actor birthday cannot be empty";
		public AddActorCommandValidation()
		{
			RuleFor(s => s.Name)
			.NotEmpty()
			.WithMessage(NameIsEmptyMsg);

			RuleFor(s => s.BirthDay)
			.NotEmpty()
			.WithMessage(BirthdayIsEmptyMsg);
		}
	}
}

