using ClassifiedsApp.Core.Entities.Common;

namespace ClassifiedsApp.Core.Entities;

public class FeaturedAdTransaction : BaseEntity
{
	public Guid AdId { get; set; }
	public Ad Ad { get; set; }

	public Guid AppUserId { get; set; }
	public AppUser AppUser { get; set; }

	public decimal Amount { get; set; }
	public int DurationDays { get; set; }
	public string PaymentReference { get; set; }
	public bool IsCompleted { get; set; }
}