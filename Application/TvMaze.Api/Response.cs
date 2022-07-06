namespace TvMaze.Api;

public class Response<T>
{
    public T Data { get; set; }
    public bool Succeeded { get; set; }
    public IEnumerable<string> Errors { get; set; }

    public Response(T data)
	{
        Data = data;
        Succeeded = true;
        Errors = null;
	}
}