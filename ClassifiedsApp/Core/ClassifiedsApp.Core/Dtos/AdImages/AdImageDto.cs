using ClassifiedsApp.Core.Dtos.Common;

namespace ClassifiedsApp.Core.Dtos.AdImages;

public class AdImageDto : BaseEntityDto
{
	public string Url { get; set; }
	public int SortOrder { get; set; } = 0;
	public Guid AdId { get; set; }
}