using Microsoft.EntityFrameworkCore;
using TvMaze.Core.Messages;
using TvMaze.Domain.Entities;

namespace TvMaze.Data.Contexts;

public class ShowContext : DbContext
{
    public DbSet<Show> Shows { get; set; }
    public DbSet<Actor> Actors { get; set; }

    public ShowContext(DbContextOptions<ShowContext> options)
        :base(options)
	{

	}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Ignore<Event>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShowContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        base.OnModelCreating(modelBuilder);
    }
}