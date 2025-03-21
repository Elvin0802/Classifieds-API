using ClassifiedsApp.Application.Dtos.Common;

namespace ClassifiedsApp.Application.Dtos.Ads;

public class AdSubCategoryValueDto : BaseEntityDto
{
	public string Value { get; set; }
	public Guid AdId { get; set; }
	public Guid SubCategoryId { get; set; }
}

public class CreateAdSubCategoryValueDto
{
	public string Value { get; set; }
	public Guid SubCategoryId { get; set; }
}
