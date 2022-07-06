namespace TvMaze.Api;

public interface IUriService
{
	public Uri GetPageUri(PaginationFilter filter, string route);
}