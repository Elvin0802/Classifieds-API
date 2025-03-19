using ClassifiedsApp.Core.Interfaces.Services.Cache;
using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Locations.GetAllLocations;

public class GetAllLocationsQuery : IRequest<GetAllLocationsQueryResponse>, ICacheableQuery
{
	// pagination
	public int PageNumber { get; set; } = 1;
	public int PageSize { get; set; } = 10;
	public string? SortBy { get; set; }
	public bool IsDescending { get; set; }

	// caching
	public string CacheKey => $"locations_page_{PageNumber}_size_{PageSize}_sort_{SortBy ?? "default"}_{(IsDescending ? "desc" : "asc")}";
	public TimeSpan CacheTime => TimeSpan.FromSeconds(40); // seconds for test / change to minute.
}
