using ClassifiedsApp.Core.Dtos.Common;

namespace ClassifiedsApp.Core.Dtos.Categories;

public class CategoryDto : BaseCategoryDto
{
	public IList<MainCategoryDto> MainCategories { get; set; }
}
