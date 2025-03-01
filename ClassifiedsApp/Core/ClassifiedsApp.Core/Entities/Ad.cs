using ClassifiedsApp.Core.Entities.Common;
using ClassifiedsApp.Core.Enums;

namespace ClassifiedsApp.Core.Entities;

public class Ad : BaseEntity
{
	public string Title { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }

	public DateTimeOffset ExpiresAt { get; set; }

	public AdStatus Status { get; set; }
	public long ViewCount { get; set; }

	public Guid CategoryId { get; set; }
	public Category Category { get; set; }
	public Guid LocationId { get; set; }
	public Location Location { get; set; }
	public Guid AppUserId { get; set; }
	public AppUser AppUser { get; set; }

	public IList<AdImage> Images { get; set; }

	public Ad()
	{
		Title = "";
		Description = "";
		Price = 0;
		ExpiresAt = DateTimeOffset.UtcNow.AddDays(7);
		Status = AdStatus.Created;
		ViewCount = 0;
	}
}
