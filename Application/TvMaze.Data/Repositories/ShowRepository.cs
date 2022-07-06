using Microsoft.EntityFrameworkCore;
using TvMaze.Data.Contexts;
using TvMaze.Domain.Entities;
using TvMaze.Domain.Repositories;

namespace TvMaze.Data.Repositories;

public class ShowRepository : IShowRepository
{
    private readonly ShowContext _showContext;
	public ShowRepository(ShowContext context)
	{
        _showContext = context;
	}

    public async Task<Guid> Add(Show show)
    {
        var s = await _showContext.AddAsync(show);
        await _showContext.SaveChangesAsync();
        return s.Entity.Id;
    }

    public async Task<Guid> AddActor(Actor actor)
    {
        var a = await _showContext.Actors.AddAsync(actor);
        await _showContext.SaveChangesAsync();
        return a.Entity.Id;
    }

    public async Task<Actor?> GetActorByShowIdAndActorName(Guid showDomainId, string actorName)
    {
        return await _showContext.Actors
                        .AsNoTracking()
                        .FirstOrDefaultAsync(a =>
                            a.ShowId.Equals(showDomainId) &&
                            a.Name.Equals(actorName));
}

    public async Task<IEnumerable<Show>> GetAll(int pageNumber, int pageSize)
    {
        return await _showContext.Shows
                        .AsNoTracking()
                        .Include(show => show.Cast)
                        .Skip((pageNumber -1)* pageSize)
                        .Take(pageSize)
                        .ToListAsync();
    }

    public async Task<int> CountShows()
    {
        return  await _showContext.Shows.CountAsync();
    }

    public async Task<Show?> GetShowByName(string name)
    {
        return await _showContext
                        .Shows
                        .AsNoTracking()
                        .Where(s => s.Name.Equals(name))
                        .Include(s => s.Cast)
                        .FirstOrDefaultAsync();
    }


    public void Dispose()
    {
        _showContext.Dispose();
    }
}

