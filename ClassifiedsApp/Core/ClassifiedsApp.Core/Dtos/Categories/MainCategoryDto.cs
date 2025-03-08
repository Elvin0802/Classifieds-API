using ClassifiedsApp.Core.Dtos.Common;

namespace ClassifiedsApp.Core.Dtos.Categories;

public class MainCategoryDto : BaseCategoryDto
{
	public Guid ParentCategoryId { get; set; }
	public IList<SubCategoryDto> SubCategories { get; set; }
}
