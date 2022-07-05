namespace TvMaze.Domain.Repositories
{
	public interface IShowRepository : IRepository<Show>
	{
		Task<Guid> Add(Show show);
		Task<Guid> AddActor(Actor actor);
		Task<Actor> GetActorByShowIdAndActorName(Guid showDomainId, string actorName);
		Task<IEnumerable<Show>> GetAll();
		Task<Show> GetShowByName(string name);
	}
}

