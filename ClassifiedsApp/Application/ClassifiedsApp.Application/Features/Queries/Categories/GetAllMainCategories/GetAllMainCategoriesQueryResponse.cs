using ClassifiedsApp.Core.Dtos.Categories;

namespace ClassifiedsApp.Application.Features.Queries.Categories.GetAllMainCategories;

public class GetAllMainCategoriesQueryResponse
{
	public IList<MainCategoryDto>? Items { get; set; }
	public int PageNumber { get; set; }
	public int PageSize { get; set; }
	public int TotalCount { get; set; }
	public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
