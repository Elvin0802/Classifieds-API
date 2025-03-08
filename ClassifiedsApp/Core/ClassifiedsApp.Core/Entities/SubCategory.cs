using ClassifiedsApp.Core.Entities.Common;
using ClassifiedsApp.Core.Enums;

namespace ClassifiedsApp.Core.Entities;

public class SubCategory : BaseEntity
{
	public string Name { get; set; }
	public SubCategoryType Type { get; set; }
	public bool IsRequired { get; set; }
	public bool IsSearchable { get; set; }
	public int SortOrder { get; set; }

	public IList<SubCategoryOption> Options { get; set; }

	public Guid MainCategoryId { get; set; }
	public MainCategory MainCategory { get; set; }
}
