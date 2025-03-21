using AutoMapper;
using ClassifiedsApp.Application.Dtos.AdImages;
using ClassifiedsApp.Core.Entities;

namespace ClassifiedsApp.Application.Common.Mappings;

public class AdImageMappingProfile : Profile
{
	public AdImageMappingProfile()
	{
		CreateMap<AdImage, AdImageDto>().ReverseMap();
	}
}
