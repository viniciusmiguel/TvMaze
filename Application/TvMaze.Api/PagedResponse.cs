namespace TvMaze.Api;

public class PagedResponse<T> : Response<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public Uri FirstPage { get; set; }
    public Uri LastPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
    public Uri NextPage { get; set; }
    public Uri PreviousPage { get; set; }
    public PagedResponse(T data, int pageNumber, int pageSize)
		:base(data)
	{
        PageNumber = pageNumber;
        PageSize = pageSize;
	}
}

public static class PaginationHelper
{
    public static PagedResponse<T> CreatePagedReponse<T>(T pagedData, PaginationFilter validFilter, int totalRecords, IUriService uriService, string route)
    {
        var respose = new PagedResponse<T>(pagedData, validFilter.PageNumber, validFilter.PageSize);
        var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
        int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
        respose.NextPage =
            validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
            ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize), route)
            : null;
        respose.PreviousPage =
            validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
            ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber - 1, validFilter.PageSize), route)
            : null;
        respose.FirstPage = uriService.GetPageUri(new PaginationFilter(1, validFilter.PageSize), route);
        respose.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, validFilter.PageSize), route);
        respose.TotalPages = roundedTotalPages;
        respose.TotalRecords = totalRecords;
        return respose;
    }
}
