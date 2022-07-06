namespace TvMaze.Application;

public interface IShowAppService
{
    Task<int> GetNumberOfShows();
    Task<IEnumerable<ShowViewModel>?> GetAllShowsAndActors(int pageNumber, int pageSize);
    Task<Guid> LocateOrCreateShow(string name, CancellationToken cancellationToken);
    Task IncludeActorIfNotExists(Guid showDomainId, string name, DateOnly birthday, CancellationToken cancellationToken);
}