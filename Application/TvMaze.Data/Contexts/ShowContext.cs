using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using TvMaze.Core;
using TvMaze.Core.Messages;
using TvMaze.Domain.Entities;

namespace TvMaze.Data.Contexts;

public class ShowContext : DbContext
{
    public DbSet<Show> Shows { get; set; }
    public DbSet<Actor> Actors { get; set; }
    private readonly ISettingsReader _settingsReader;
    public ShowContext(ISettingsReader settingsReader)
	{
        _settingsReader = settingsReader;
	}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_settingsReader.GetSetting("SqlConnectionString"));
        optionsBuilder.LogTo(
            Console.WriteLine,
            LogLevel.Warning,
            DbContextLoggerOptions.DefaultWithUtcTime | DbContextLoggerOptions.SingleLine);
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