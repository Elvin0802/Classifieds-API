namespace ClassifiedsApp.Application.Dtos.Ads;

// dto for showing ad preview. ( ad in ads grid. )
public class AdPreviewDto
{
	public Guid Id { get; set; }
	public string Title { get; set; }
	public decimal Price { get; set; }
	public bool IsNew { get; set; }
	public bool IsSelected { get; set; }
	public bool IsFeatured { get; set; }
	public string LocationCityName { get; set; }
	public string MainImageUrl { get; set; }
	public DateTimeOffset UpdatedAt { get; set; }
}