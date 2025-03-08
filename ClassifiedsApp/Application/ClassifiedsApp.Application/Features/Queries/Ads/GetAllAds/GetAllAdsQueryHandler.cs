using ClassifiedsApp.Core.Dtos.Ads;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Interfaces.Repositories.Ads;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassifiedsApp.Application.Features.Queries.Ads.GetAllAds;

/*
public class GetAllAdsQueryHandler : IRequestHandler<GetAllAdsQuery, GetAllAdsQueryResponse>
{
	readonly IAdReadRepository _repository;

	public GetAllAdsQueryHandler(IAdReadRepository repository)
	{
		_repository = repository;
	}

	public async Task<GetAllAdsQueryResponse> Handle(GetAllAdsQuery request, CancellationToken cancellationToken)
	{
		var query = _repository.GetAll(false)
								.Where(ad => ad.Status == Core.Enums.AdStatus.Active) // sadece aktiv elanlar gosterilir.
								.Include(ad => ad.Images)
								.Include(ad => ad.Location)
								.OrderByDescending(p => p.CreatedAt); // yaradilma tarixine gore yeniden kohneye dogru siralamaq.

		var totalCount = await query.CountAsync(cancellationToken);

		var list = await query.Skip((request.PageNumber - 1) * request.PageSize)
							  .Take(request.PageSize)
							  .Select(p => new AdPreviewDto
							  {
								  Id = p.Id,
								  Title = p.Title,
								  Price = p.Price,
								  LocationCityName = p.Location.City,
								  MainImageUrl = p.Images.FirstOrDefault(img => img.SortOrder == 0)!.Url,
								  UpdatedAt = p.UpdatedAt,
							  })
							  .ToListAsync(cancellationToken);

		return new GetAllAdsQueryResponse
		{
			Items = list,
			PageNumber = request.PageNumber,
			PageSize = request.PageSize,
			TotalCount = totalCount
		};
	}
}
*/


public class GetAllAdsQueryHandler : IRequestHandler<GetAllAdsQuery, GetAllAdsQueryResponse>
{
	private readonly IAdReadRepository _repository;

	public GetAllAdsQueryHandler(IAdReadRepository repository)
	{
		_repository = repository;
	}

	public async Task<GetAllAdsQueryResponse> Handle(GetAllAdsQuery request, CancellationToken cancellationToken)
	{
		// Start with base query
		var query = _repository.GetAll(false)
			.Where(ad => ad.Status == Core.Enums.AdStatus.Active)
			.Include(ad => ad.Images)
			.Include(ad => ad.Location)
			.Include(ad => ad.Category)
			.Include(ad => ad.MainCategory)
			.Include(ad => ad.SubCategoryValues)
				.ThenInclude(scv => scv.SubCategory)
			.AsQueryable();


		// Apply search filter
		var searchTerm = request.SearchTitle?.Trim().ToLower();

		if (!string.IsNullOrWhiteSpace(searchTerm))
			query = query.Where(ad => ad.Title.ToLower().Contains(searchTerm));

		// Apply price filters
		if (request.MinPrice.HasValue)
			query = query.Where(ad => ad.Price >= request.MinPrice.Value);

		if (request.MaxPrice.HasValue)
			query = query.Where(ad => ad.Price <= request.MaxPrice.Value);

		// Apply category filters
		if (request.CategoryId.HasValue)
			query = query.Where(ad => ad.CategoryId == request.CategoryId.Value);

		if (request.MainCategoryId.HasValue)
			query = query.Where(ad => ad.MainCategoryId == request.MainCategoryId.Value);

		// Apply location filters
		if (request.LocationId.HasValue)
			query = query.Where(ad => ad.LocationId == request.LocationId.Value);

		if (request.SubCategoryValues is not null && request.SubCategoryValues.Count > 0)
		{
			foreach (var v in request.SubCategoryValues)
			{
				query = query.Where(ad => ad.SubCategoryValues.Any(scv => scv.SubCategoryId == v.Key && scv.Value == v.Value));
			}
		}

		// Apply sorting
		query = ApplySorting(query, request.SortBy, request.IsDescending);

		// Get total count before pagination
		var totalCount = await query.CountAsync(cancellationToken);

		// Apply pagination
		var paginatedQuery = query
			.Skip((request.PageNumber - 1) * request.PageSize)
			.Take(request.PageSize);

		// Get results
		var list = await paginatedQuery
			.Select(p => new AdPreviewDto
			{
				Id = p.Id,
				Title = p.Title,
				Price = p.Price,
				LocationCityName = p.Location.City,
				MainImageUrl = p.Images.FirstOrDefault(img => img.SortOrder == 0)!.Url,
				UpdatedAt = p.UpdatedAt,
			})
			.ToListAsync(cancellationToken);

		return new GetAllAdsQueryResponse
		{
			Items = list,
			PageNumber = request.PageNumber,
			PageSize = request.PageSize,
			TotalCount = totalCount
		};
	}

	private IQueryable<Ad> ApplySorting(IQueryable<Ad> query, string? sortBy, bool isDescending)
	{
		if (string.IsNullOrWhiteSpace(sortBy))
		{
			// Default sorting
			return isDescending
				? query.OrderByDescending(p => p.CreatedAt)
				: query.OrderBy(p => p.CreatedAt);
		}

		// Apply dynamic sorting based on property name
		switch (sortBy.ToLower())
		{
			case "price":
				return isDescending
					? query.OrderByDescending(p => p.Price)
					: query.OrderBy(p => p.Price);

			case "title":
				return isDescending
					? query.OrderByDescending(p => p.Title)
					: query.OrderBy(p => p.Title);

			case "updatedat":
				return isDescending
					? query.OrderByDescending(p => p.UpdatedAt)
					: query.OrderBy(p => p.UpdatedAt);

			case "viewcount":
				return isDescending
					? query.OrderByDescending(p => p.ViewCount)
					: query.OrderBy(p => p.ViewCount);

			default:
				return isDescending
					? query.OrderByDescending(p => p.CreatedAt)
					: query.OrderBy(p => p.CreatedAt);
		}
	}
}