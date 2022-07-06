namespace TvMaze.Application;

public class ShowAppService : IShowAppService
{
    private readonly IShowRepository _showRepository;
    private readonly IMediator _mediator;
	public ShowAppService(IShowRepository showRepository,
                            IMediator mediator)
	{
        _showRepository = showRepository;
        _mediator = mediator;
	}
    public async Task<int> GetNumberOfShows()
    {
        return await _showRepository.CountShows();
    }
    public async Task<IEnumerable<ShowViewModel>?> GetAllShowsAndActors(int pageNumber, int pageSize)
    {
        var shows = await _showRepository.GetAll(pageNumber, pageSize);

        if (shows is null || !shows.Any()) return null;

        var showVieModels = new List<ShowViewModel>();

        foreach(var show in shows)
        {
            var actors = new List<ActorViewModel>();
            foreach(var actor in show.Cast.OrderByDescending(p => p.BirthDay))
            {
                actors.Add(new ActorViewModel()
                {
                    Name = actor.Name,
                    BirthDay = actor.BirthDay
                });
            }
            showVieModels.Add(new ShowViewModel()
            {
                Name = show.Name,
                Cast = actors
            });
        }

        return showVieModels;
    }

    public async Task IncludeActorIfNotExists(Guid showDomainId, string actorName, DateOnly birthday, CancellationToken cancellationToken)
    {
        var actor = await _showRepository.GetActorByShowIdAndActorName(showDomainId, actorName);

        if (actor is object) return;

        var cmd = new AddActorCommand(actorName, birthday, showDomainId);
        await _mediator.Send(cmd);

        return;
    }

    public async Task<Guid> LocateOrCreateShow(string name, CancellationToken cancellationToken)
    {
        var show = await _showRepository.GetShowByName(name);
        if (show is object) return show.Id;
        var cmd = new AddShowCommand(name);
        await _mediator.Send(cmd);
        return (await _showRepository.GetShowByName(name)).Id;
    }
}