namespace TvMaze.Core.Data;

public interface IRepository<T> : IDisposable where T : IAggregateRoot
{

}

