using ClassifiedsApp.Core.Entities.Common;

namespace ClassifiedsApp.Core.Entities;

public class MainCategory : BaseCategory
{
	public Guid ParentCategoryId { get; set; }
	public Category ParentCategory { get; set; }

	public IList<SubCategory> SubCategories { get; set; }
}
