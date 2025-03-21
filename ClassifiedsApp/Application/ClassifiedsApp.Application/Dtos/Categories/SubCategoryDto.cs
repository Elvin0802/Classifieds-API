using ClassifiedsApp.Application.Dtos.Common;
using ClassifiedsApp.Core.Enums;

namespace ClassifiedsApp.Application.Dtos.Categories;

public class SubCategoryDto : BaseEntityDto
{
	public string Name { get; set; }
	public SubCategoryType Type { get; set; }
	public bool IsRequired { get; set; }
	public bool IsSearchable { get; set; }
	public int SortOrder { get; set; }
	public Guid MainCategoryId { get; set; }

	public IList<SubCategoryOptionDto> Options { get; set; }
}