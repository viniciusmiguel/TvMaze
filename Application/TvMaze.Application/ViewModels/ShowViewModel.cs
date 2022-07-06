namespace TvMaze.Application.ViewModels;

public class ShowViewModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public IEnumerable<ActorViewModel>? Cast { get; set; }
}