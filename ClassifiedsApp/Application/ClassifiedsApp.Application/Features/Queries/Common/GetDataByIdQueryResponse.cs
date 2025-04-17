namespace ClassifiedsApp.Application.Features.Queries.Common;

public class GetDataByIdQueryResponse<T> where T : class
{
	public T? Item { get; set; }
}