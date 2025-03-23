using AutoMapper;
using ClassifiedsApp.Application.Dtos.AdImages;
using ClassifiedsApp.Application.Dtos.Ads;
using ClassifiedsApp.Application.Dtos.Auth.Users;
using ClassifiedsApp.Application.Dtos.Categories;
using ClassifiedsApp.Application.Dtos.Locations;
using ClassifiedsApp.Application.Interfaces.Repositories.Ads;
using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Ads.GetAdById;

public class GetAdByIdQueryHandler : IRequestHandler<GetAdByIdQuery, GetAdByIdResponse>
{
	readonly IAdReadRepository _readRepository;
	readonly IAdWriteRepository _writeRepository;
	readonly IMapper _mapper;

	public GetAdByIdQueryHandler(IAdReadRepository readRepository, IAdWriteRepository writeRepository, IMapper mapper)
	{
		_readRepository = readRepository;
		_writeRepository = writeRepository;
		_mapper = mapper;
	}

	public async Task<GetAdByIdResponse> Handle(GetAdByIdQuery request, CancellationToken cancellationToken)
	{
		var item = await _readRepository.GetAdByIdWithIncludesAsync(request.Id, true);

		item.ViewCount++;

		_writeRepository.Update(item);

		await _writeRepository.SaveAsync();

		if (item is null) return new() { AdDto = null };

		return new()
		{
			//AdDto = _mapper.Map<AdDto>(item) // mapper i config et.

			AdDto = new()
			{
				Id = item.Id,
				CreatedAt = item.CreatedAt,
				UpdatedAt = item.UpdatedAt,
				Title = item.Title,
				Description = item.Description,
				Price = item.Price,
				ViewCount = item.ViewCount,
				Category = _mapper.Map<CategoryDto>(item.Category),
				MainCategory =  _mapper.Map<MainCategoryDto>(item.MainCategory),
				Location = _mapper.Map<LocationDto>(item.Location),
				AppUser = _mapper.Map<AppUserDto>(item.AppUser),

				IsOwner = item.AppUserId == request.CurrentUserId,
				IsNew = item.IsNew,
				IsFeatured = item.IsFeatured,
				FeatureEndDate = item.FeatureEndDate,
				SelectorUsersCount = item.SelectorUsers.Count,

				Images = item.Images.Select(img => _mapper.Map<AdImageDto>(img)).ToList(),
				AdSubCategoryValues = item.SubCategoryValues.Select(ascv => _mapper.Map<AdSubCategoryValueDto>(ascv)).ToList()

			}
		};
	}
}
