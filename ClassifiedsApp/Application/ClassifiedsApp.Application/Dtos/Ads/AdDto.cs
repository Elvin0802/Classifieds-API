using ClassifiedsApp.Application.Dtos.AdImages;
using ClassifiedsApp.Application.Dtos.Auth.Users;
using ClassifiedsApp.Application.Dtos.Categories;
using ClassifiedsApp.Application.Dtos.Common;
using ClassifiedsApp.Application.Dtos.Locations;

namespace ClassifiedsApp.Application.Dtos.Ads;

public class AdDto : BaseEntityDto
{
	public string Title { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	public bool IsNew { get; set; }
	public bool IsFeatured { get; set; } = false;
	public bool IsOwner { get; set; } = false;
	public DateTimeOffset ExpiresAt { get; set; }
	public DateTimeOffset? FeatureEndDate { get; set; }
	public long ViewCount { get; set; }
	public long SelectorUsersCount { get; set; }
	public CategoryDto Category { get; set; }
	public MainCategoryDto MainCategory { get; set; }
	public LocationDto Location { get; set; }
	public AppUserDto AppUser { get; set; }
	public IList<AdImageDto> Images { get; set; }
	public IList<AdSubCategoryValueDto> AdSubCategoryValues { get; set; }

}
