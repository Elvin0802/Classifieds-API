using ClassifiedsApp.Application.Dtos.Common;

namespace ClassifiedsApp.Application.Dtos.Categories;

public class CategoryDto : BaseCategoryDto
{
	public IList<MainCategoryDto> MainCategories { get; set; }
}
