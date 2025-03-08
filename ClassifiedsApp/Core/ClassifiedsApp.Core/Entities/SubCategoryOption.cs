using ClassifiedsApp.Core.Entities.Common;

namespace ClassifiedsApp.Core.Entities;

public class SubCategoryOption : BaseEntity
{
	public string Value { get; set; }
	public int SortOrder { get; set; }

	public Guid SubCategoryId { get; set; }
	public SubCategory SubCategory { get; set; }
}
