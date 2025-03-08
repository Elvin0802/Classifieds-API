using ClassifiedsApp.Core.Dtos.Locations;

namespace ClassifiedsApp.Application.Features.Queries.Locations.GetAllLocations;

public class GetAllLocationsQueryResponse
{
	public IList<LocationDto>? Items { get; set; }
	public int PageNumber { get; set; }
	public int PageSize { get; set; }
	public int TotalCount { get; set; }
	public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
