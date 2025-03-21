using AutoMapper;
using ClassifiedsApp.Application.Dtos.Categories;
using ClassifiedsApp.Core.Entities;

namespace ClassifiedsApp.Application.Common.Mappings;

public class CategoriesMappingProfile : Profile
{
	public CategoriesMappingProfile()
	{
		CreateMap<Category, CategoryDto>().ReverseMap();
		CreateMap<MainCategory, MainCategoryDto>().ReverseMap();
		CreateMap<SubCategory, SubCategoryDto>().ReverseMap();

		//CreateMap<Category, CategoryDto>()
		//   .ForMember(dest => dest.MainCategories, opt => opt.MapFrom(src => src.MainCategories)) // Ensure list mapping
		//   .ReverseMap();

		//CreateMap<MainCategory, MainCategoryDto>()
		//	.ForMember(dest => dest.SubCategories, opt => opt.MapFrom(src => src.SubCategories)) // Ensure SubCategories mapping
		//	.ReverseMap();

		//CreateMap<SubCategory, SubCategoryDto>()
		//	.ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options)) // Ensure Options mapping
		//	.ReverseMap();

		CreateMap<SubCategoryOption, SubCategoryOptionDto>().ReverseMap();
	}
}
