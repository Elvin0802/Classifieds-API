using ClassifiedsApp.Application.Common.Results;
using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Locations.GetAllLocations;

public class GetAllLocationsQuery : IRequest<Result<GetAllLocationsQueryResponse>>
{
}

//---------------------------

// caching usage.

/*

public class GetAllLocationsQuery : IRequest<Result<GetAllLocationsQueryResponse>>, ICacheableQuery
{
	// caching
	public string CacheKey => $"{nameof(GetAllLocationsQuery)}";
	public TimeSpan CacheTime => TimeSpan.FromSeconds(20); // seconds for test / change to minute.
}

//	public string CacheKey => $"{nameof(GetAllLocationsQuery)}_page_{PageNumber}_size_{PageSize}_sort_{SortBy ?? "default"}_{(IsDescending ? "desc" : "asc")}";

*/