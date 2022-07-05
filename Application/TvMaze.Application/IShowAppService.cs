namespace TvMaze.Application;

public interface IShowAppService
{
    Task<IEnumerable<ShowViewModel>?> GetAllShowsAndActors();
    Task<Guid> LocateOrCreateShow(string name, CancellationToken cancellationToken);
    Task IncludeActorIfNotExists(Guid showDomainId, string name, DateOnly birthday, CancellationToken cancellationToken);
}