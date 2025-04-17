namespace ClassifiedsApp.Application.Features.Queries.Common;

public class GetAllDataQueryResponse<T> where T : class
{
	public IList<T>? Items { get; set; }
	public int PageNumber { get; set; }
	public int PageSize { get; set; }
	public int TotalCount { get; set; }
	public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}