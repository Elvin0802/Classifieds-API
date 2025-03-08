using ClassifiedsApp.Core.Dtos.Categories;

namespace ClassifiedsApp.Application.Features.Queries.Categories.GetAllSubCategories;

public class GetAllSubCategoriesQueryResponse
{
	public IList<SubCategoryDto>? Items { get; set; }
	public int PageNumber { get; set; }
	public int PageSize { get; set; }
	public int TotalCount { get; set; }
	public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
