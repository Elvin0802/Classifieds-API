using ClassifiedsApp.Core.Dtos.Categories;

namespace ClassifiedsApp.Application.Features.Queries.Categories.GetAllCategories;

public class GetAllCategoriesQueryResponse
{
	public IList<CategoryDto>? Items { get; set; }
	public int PageNumber { get; set; }
	public int PageSize { get; set; }
	public int TotalCount { get; set; }
	public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
