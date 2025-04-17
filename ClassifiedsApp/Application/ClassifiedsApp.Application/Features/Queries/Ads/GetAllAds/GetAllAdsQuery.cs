using ClassifiedsApp.Application.Features.Queries.Common;
using ClassifiedsApp.Core.Enums;

namespace ClassifiedsApp.Application.Features.Queries.Ads.GetAllAds;


// BaseGetAllQuery
/*
public class GetAllAdsQuery : IRequest<GetAllAdsQueryResponse>
{
	public int PageNumber { get; set; } = 1;
	public int PageSize { get; set; } = 10;
	public string? SortBy { get; set; }
	public bool IsDescending { get; set; }
}
*/


public class GetAllAdsQuery : GetAllDataQuery<GetAllAdsQueryResponse>
{
	public string? SearchTitle { get; set; }
	public bool IsFeatured { get; set; }

	public decimal? MinPrice { get; set; }
	public decimal? MaxPrice { get; set; }
	public Guid? CategoryId { get; set; }
	public Guid? MainCategoryId { get; set; }
	public Guid? LocationId { get; set; }

	public Guid? CurrentAppUserId { get; set; }
	public Guid? SearchedAppUserId { get; set; }
	public AdStatus? AdStatus { get; set; }

	public Dictionary<Guid, string>? SubCategoryValues { get; set; }

}