using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Interfaces.Services.Ads;
using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Ads.GetFeaturedPricing;

public class GetFeaturedPricingQueryHandler : IRequestHandler<GetFeaturedPricingQuery, Result<GetFeaturedPricingQueryResponse>>
{
	readonly IFeaturedAdService _featuredAdService;

	public GetFeaturedPricingQueryHandler(IFeaturedAdService featuredAdService)
	{
		_featuredAdService = featuredAdService;
	}

	public async Task<Result<GetFeaturedPricingQueryResponse>> Handle(GetFeaturedPricingQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var items = await _featuredAdService.GetFeaturePricingOptionsAsync();

			var data = new GetFeaturedPricingQueryResponse
			{
				Items = items,
				PageNumber = 1,
				PageSize = items.Count,
				TotalCount = items.Count
			};

			return Result.Success(data, "Featured pricings retrieved successfully.");
		}
		catch (Exception ex)
		{
			return Result.Failure<GetFeaturedPricingQueryResponse>($"Failed to retrieve featured pricings: {ex.Message}");
		}
	}
}
