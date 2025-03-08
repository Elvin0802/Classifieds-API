using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Ads.GetAllAds;

/*
public class GetAllAdsQuery : IRequest<GetAllAdsQueryResponse>
{
	public int PageNumber { get; set; } = 1;
	public int PageSize { get; set; } = 10;
	public string? SortBy { get; set; }
	public bool IsDescending { get; set; }
}
*/


public class GetAllAdsQuery : IRequest<GetAllAdsQueryResponse>
{
	// Pagination
	public int PageNumber { get; set; } = 1;
	public int PageSize { get; set; } = 10;

	// Sorting
	public string? SortBy { get; set; }
	public bool IsDescending { get; set; } = true;

	// Search
	public string? SearchTitle { get; set; }

	// Filters
	public decimal? MinPrice { get; set; }
	public decimal? MaxPrice { get; set; }
	public Guid? CategoryId { get; set; }
	public Guid? MainCategoryId { get; set; }
	public Guid? LocationId { get; set; }

	// Additional sub category filters
	public Dictionary<Guid, string>? SubCategoryValues { get; set; } // Guid = Marka , string = BMW

}