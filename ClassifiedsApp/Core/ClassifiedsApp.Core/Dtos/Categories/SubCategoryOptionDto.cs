using ClassifiedsApp.Core.Dtos.Common;

namespace ClassifiedsApp.Core.Dtos.Categories;

public class SubCategoryOptionDto : BaseEntityDto
{
	public string Value { get; set; }
	public int SortOrder { get; set; }
	public Guid SubCategoryId { get; set; }
}
