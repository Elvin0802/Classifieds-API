using ClassifiedsApp.Core.Entities.Common;

namespace ClassifiedsApp.Core.Entities;

public class UserAdSelection : BaseEntity
{
	public Guid AppUserId { get; set; }
	public AppUser AppUser { get; set; }

	public Guid AdId { get; set; }
	public Ad Ad { get; set; }
}