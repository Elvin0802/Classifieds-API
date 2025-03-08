using AutoMapper;
using ClassifiedsApp.Application.Features.Commands.Locations.CreateLocation;
using ClassifiedsApp.Core.Dtos.Locations;
using ClassifiedsApp.Core.Entities;

namespace ClassifiedsApp.Application.Common.Mappings;

public class LocationMappingProfile : Profile
{
	public LocationMappingProfile()
	{
		CreateMap<CreateLocationCommand, Location>();

		CreateMap<Location, LocationDto>().ReverseMap();

	}
}