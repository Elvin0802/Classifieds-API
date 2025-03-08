namespace ClassifiedsApp.Core.Entities.Common;

public class BaseCategory : BaseEntity
{
	public string Name { get; set; }
	public string Slug { get; set; }
	public IList<Ad> Ads { get; set; }
}
