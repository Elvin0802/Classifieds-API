using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Dtos.Ads;
using ClassifiedsApp.Application.Interfaces.Repositories.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassifiedsApp.Application.Features.Queries.Users.GetAllSelectedAds;

public class GetAllSelectedAdsQueryHandler : IRequestHandler<GetAllSelectedAdsQuery, Result<GetAllSelectedAdsQueryResponse>>
{
	readonly IUserAdSelectionReadRepository _readRepository;

	public GetAllSelectedAdsQueryHandler(IUserAdSelectionReadRepository readRepository)
	{
		_readRepository = readRepository;
	}

	public async Task<Result<GetAllSelectedAdsQueryResponse>> Handle(GetAllSelectedAdsQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var query = _readRepository.GetAll(false)
									   .Where(uas => uas.AppUserId == request.CurrentAppUserId)
									   .Include(uas => uas.Ad)
										.ThenInclude(ad => ad.Location)
									   .Include(uas => uas.Ad)
										.ThenInclude(ad => ad.Images)
									   .Include(uas => uas.AppUser)
									   .AsQueryable();

			var totalCount = await query.CountAsync(cancellationToken);

			var paginatedQuery = query
				.Skip((request.PageNumber - 1) * request.PageSize)
				.Take(request.PageSize);

			var list = await paginatedQuery
				.Select(uas => new AdPreviewDto
				{
					Id = uas.Ad.Id,
					Title = uas.Ad.Title,
					Price = uas.Ad.Price,
					LocationCityName = uas.Ad.Location.City,
					MainImageUrl = uas.Ad.Images.FirstOrDefault(img => img.SortOrder == 0)!.Url,
					IsSelected = true,
					UpdatedAt = uas.Ad.UpdatedAt,
				})
				.ToListAsync(cancellationToken);

			var data = new GetAllSelectedAdsQueryResponse
			{
				Items = list,
				PageNumber = request.PageNumber,
				PageSize = request.PageSize,
				TotalCount = totalCount
			};

			return Result.Success(data, "Selected ads retrieved successfully.");
		}
		catch (Exception ex)
		{
			return Result.Failure<GetAllSelectedAdsQueryResponse>($"Failed to retrieve selected ads: {ex.Message}");
		}
	}
}
