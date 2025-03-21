using ClassifiedsApp.Application.Dtos.Common;

namespace ClassifiedsApp.Application.Dtos.Categories;

public class SubCategoryOptionDto : BaseEntityDto
{
	public string Value { get; set; }
	public int SortOrder { get; set; }
	public Guid SubCategoryId { get; set; }
}
