using ClassifiedsApp.Core.Entities;

namespace ClassifiedsApp.Application.Features.Queries.Categories.GetAllCategories;

public class CategoryQueryResponse
{
	public IList<Category> Items { get; set; }
	public int PageNumber { get; set; }
	public int PageSize { get; set; }
	public int TotalCount { get; set; }
	public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
