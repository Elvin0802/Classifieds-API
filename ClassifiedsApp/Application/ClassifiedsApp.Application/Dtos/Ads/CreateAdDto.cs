namespace ClassifiedsApp.Application.Dtos.Ads;

public class CreateAdDto
{
	public string Title { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	public bool IsNew { get; set; }
	public DateTimeOffset ExpiresAt { get; set; }
	public Guid CategoryId { get; set; }
	public Guid MainCategoryId { get; set; }
	public Guid LocationId { get; set; }
	public Guid AppUserId { get; set; }
}
