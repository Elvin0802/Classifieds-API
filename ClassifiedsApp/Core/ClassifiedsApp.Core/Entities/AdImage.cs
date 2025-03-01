using ClassifiedsApp.Core.Entities.Common;

namespace ClassifiedsApp.Core.Entities;

public class AdImage : BaseEntity
{
	public string Url { get; set; } = ""; // base url of image in web
	public int SortOrder { get; set; } = 0; // if SortOrder == 0 { this image is the main image }
	public Guid AdId { get; set; }
	public Ad Ad { get; set; }
}
