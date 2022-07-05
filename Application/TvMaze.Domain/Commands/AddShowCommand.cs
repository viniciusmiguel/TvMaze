namespace TvMaze.Domain.Commands;

public class AddShowCommand : Command
{
    public string Name { get; private set; }
    public AddShowCommand(string name)
    {
        Name = name;
    }
    public override bool IsValid()
    {
        ValidationResult = new AddShowCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}