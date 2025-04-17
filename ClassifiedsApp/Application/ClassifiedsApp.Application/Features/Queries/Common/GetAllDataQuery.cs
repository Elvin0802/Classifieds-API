using ClassifiedsApp.Application.Common.Results;
using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Common;

public class GetAllDataQuery<T> : IRequest<Result<T>> where T : class
{
	// pagination
	public int PageNumber { get; set; } = 1;
	public int PageSize { get; set; } = 10;
	public string? SortBy { get; set; }
	public bool IsDescending { get; set; }
}