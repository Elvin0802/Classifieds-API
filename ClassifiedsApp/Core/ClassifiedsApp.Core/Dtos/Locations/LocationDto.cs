using ClassifiedsApp.Core.Dtos.Common;

namespace ClassifiedsApp.Core.Dtos.Locations;

public class LocationDto : BaseEntityDto
{
	public string City { get; set; }
	public string Country { get; set; }
}