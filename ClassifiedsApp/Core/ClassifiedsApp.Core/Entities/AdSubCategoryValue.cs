using ClassifiedsApp.Core.Entities.Common;

namespace ClassifiedsApp.Core.Entities;

public class AdSubCategoryValue : BaseEntity
{
	public string Value { get; set; }

	public Guid AdId { get; set; }
	public Ad Ad { get; set; }

	public Guid SubCategoryId { get; set; }
	public SubCategory SubCategory { get; set; }
}