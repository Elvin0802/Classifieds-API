using ClassifiedsApp.Core.Entities.Common;
using ClassifiedsApp.Core.Enums;

namespace ClassifiedsApp.Core.Entities;

public class Ad : BaseEntity
{
	public string Title { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	public bool IsNew { get; set; }

	public DateTimeOffset ExpiresAt { get; set; }

	public AdStatus Status { get; set; }
	public long ViewCount { get; set; }

	public Guid CategoryId { get; set; }
	public Category Category { get; set; }

	public Guid MainCategoryId { get; set; }
	public MainCategory MainCategory { get; set; }

	public Guid LocationId { get; set; }
	public Location Location { get; set; }

	public Guid AppUserId { get; set; }
	public AppUser AppUser { get; set; }

	public IList<AdImage> Images { get; set; }
	public IList<AdSubCategoryValue> SubCategoryValues { get; set; }
	public IList<UserAdSelection> SelectorUsers { get; set; }

	/* Feature system */
	public bool IsFeatured { get; set; }
	public DateTimeOffset? FeatureStartDate { get; set; }
	public DateTimeOffset? FeatureEndDate { get; set; }
	public int? FeaturePriority { get; set; } // Higher priority ads appear first

	public IList<FeaturedAdTransaction> FeatureTransactions { get; set; }

	public Ad()
	{
		Title = "";
		Description = "";
		Price = 0;
		IsNew = false;
		ExpiresAt = DateTimeOffset.MinValue;
		Status = AdStatus.Rejected;
		ViewCount = 0;
		IsFeatured = false;
		FeaturePriority = 0;
	}
}