namespace TvMaze.Application.ViewModels;

public class ShowViewModel
{
    public string? Name { get; set; }
    public IEnumerable<ActorViewModel>? Cast { get; set; }
}