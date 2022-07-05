namespace TvMaze.Core.Extensions;

public static class CollectionExtensions
{
    public static Task ParallelForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> function, int maxDoP = 2)
    {
        async Task AwaitPartition(IEnumerator<T> partition)
        {
            using (partition)
            {
                while (partition.MoveNext())
                {
                    await Task.Yield();
                    await function(partition.Current);
                }
            }
        }

        return Task.WhenAll(
            Partitioner
                .Create(source)
                .GetPartitions(maxDoP)
                .AsParallel()
                .Select(p => AwaitPartition(p)));
    }
}

