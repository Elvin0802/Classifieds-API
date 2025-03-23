using ClassifiedsApp.Application.Dtos.Ads;

namespace ClassifiedsApp.Application.Features.Queries.Ads.GetFeaturedPricing;

public class GetFeaturedPricingQueryResponse
{
	public IList<FeaturedAdPricingDto> Items { get; set; }
}
