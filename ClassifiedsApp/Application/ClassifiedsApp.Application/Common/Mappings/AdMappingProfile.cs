using AutoMapper;
using ClassifiedsApp.Core.Dtos.Ads;
using ClassifiedsApp.Core.Entities;

namespace ClassifiedsApp.Application.Common.Mappings;

public class AdMappingProfile : Profile
{
	public AdMappingProfile()
	{
		CreateMap<Ad, AdDto>().ReverseMap();
		CreateMap<Ad, AdPreviewDto>().ReverseMap();
		CreateMap<AdSubCategoryValue, AdSubCategoryValueDto>().ReverseMap();
		CreateMap<AdSubCategoryValue, CreateAdSubCategoryValueDto>().ReverseMap();
	}
}
