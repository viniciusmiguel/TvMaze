using System;
using TvMaze.Core.Data;
using TvMaze.Domain.Entities;
using TvMaze.Domain.Repositories;

namespace TvMaze.Data.Repositories;

public class ShowRepository : IShowRepository
{
	public ShowRepository()
	{
	}

    public Task<Guid> Add(Show show)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> AddActor(Actor actor)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        
    }

    public Task<Actor> GetActorByShowIdAndActorName(Guid showDomainId, string actorName)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Show>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Show> GetShowByName(string name)
    {
        throw new NotImplementedException();
    }
}

