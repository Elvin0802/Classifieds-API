using ClassifiedsApp.Application.Dtos.Common;

namespace ClassifiedsApp.Application.Dtos.Categories;

public class MainCategoryDto : BaseCategoryDto
{
	public Guid ParentCategoryId { get; set; }
	public IList<SubCategoryDto> SubCategories { get; set; }
}
