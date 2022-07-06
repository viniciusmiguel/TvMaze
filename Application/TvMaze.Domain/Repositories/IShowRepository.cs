namespace TvMaze.Domain.Repositories
{
	public interface IShowRepository : IRepository<Show>
	{
		Task<Guid> Add(Show show);
		Task<Guid> AddActor(Actor actor);
		Task<int> CountShows();
		Task<Actor?> GetActorByShowIdAndActorName(Guid showDomainId, string actorName);
		Task<IEnumerable<Show>> GetAll(int pageNumber, int pageSize);
		Task<Show?> GetShowByName(string name);
	}
}

