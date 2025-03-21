using ClassifiedsApp.Application.Dtos.Common;

namespace ClassifiedsApp.Application.Dtos.Locations;

public class LocationDto : BaseEntityDto
{
	public string City { get; set; }
	public string Country { get; set; }
}