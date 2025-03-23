using ClassifiedsApp.Application.Interfaces.Services.Ads;
using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Ads.GetFeaturedPricing;

public class GetFeaturedPricingQueryHandler : IRequestHandler<GetFeaturedPricingQuery, GetFeaturedPricingQueryResponse>
{
	readonly IFeaturedAdService _featuredAdService;

	public GetFeaturedPricingQueryHandler(IFeaturedAdService featuredAdService)
	{
		_featuredAdService = featuredAdService;
	}

	public async Task<GetFeaturedPricingQueryResponse> Handle(GetFeaturedPricingQuery request, CancellationToken cancellationToken)
	{
		return new()
		{
			Items = await _featuredAdService.GetFeaturePricingOptionsAsync()
		};
	}
}
