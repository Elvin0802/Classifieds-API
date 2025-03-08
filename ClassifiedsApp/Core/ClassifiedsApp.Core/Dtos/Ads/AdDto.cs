using ClassifiedsApp.Core.Dtos.AdImages;
using ClassifiedsApp.Core.Dtos.Auth.Users;
using ClassifiedsApp.Core.Dtos.Categories;
using ClassifiedsApp.Core.Dtos.Common;
using ClassifiedsApp.Core.Dtos.Locations;

namespace ClassifiedsApp.Core.Dtos.Ads;

public class AdDto : BaseEntityDto
{
	public string Title { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	public long ViewCount { get; set; }
	public CategoryDto Category { get; set; }
	public MainCategoryDto MainCategory { get; set; }
	public LocationDto Location { get; set; }
	public AppUserDto AppUser { get; set; }
	public IList<AdImageDto> Images { get; set; }
	public IList<AdSubCategoryValueDto> AdSubCategoryValues { get; set; }
}
