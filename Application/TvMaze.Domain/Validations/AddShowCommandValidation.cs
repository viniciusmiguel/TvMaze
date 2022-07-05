namespace TvMaze.Domain.Validations;

public class AddShowCommandValidation : AbstractValidator<AddShowCommand>
{
	public static string NameIsEmptyMsg = "The show name cannot be empty";
	public AddShowCommandValidation()
	{
		RuleFor(s => s.Name)
			.NotEmpty()
			.WithMessage(NameIsEmptyMsg);
	}
}