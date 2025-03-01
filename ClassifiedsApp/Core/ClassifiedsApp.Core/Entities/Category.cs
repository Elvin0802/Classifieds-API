using ClassifiedsApp.Core.Entities.Common;

namespace ClassifiedsApp.Core.Entities;

public class Category : BaseEntity
{
	public string Name { get; set; }
	public string Slug { get; set; }
	public IList<Ad> Ads { get; set; }
	public IList<Category>? SubCategories { get; set; }
	public bool IsActive { get; set; }
	public bool IsSubCategory { get; set; }

	public Guid? ParentCategoryId { get; set; }
	public Category? ParentCategory { get; set; }
}
