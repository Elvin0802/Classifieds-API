using ClassifiedsApp.Core.Dtos.Ads;

namespace ClassifiedsApp.Application.Features.Queries.Ads.GetAllAds;

public class GetAllAdsQueryResponse
{
	public IList<AdPreviewDto>? Items { get; set; }
	public int PageNumber { get; set; }
	public int PageSize { get; set; }
	public int TotalCount { get; set; }
	public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
