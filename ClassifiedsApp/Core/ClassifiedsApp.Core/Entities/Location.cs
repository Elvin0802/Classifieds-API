using ClassifiedsApp.Core.Entities.Common;

namespace ClassifiedsApp.Core.Entities;

public class Location : BaseEntity
{
	public string City { get; set; }
	public string Country { get; set; }
	public IList<Ad> Ads { get; set; }
}
