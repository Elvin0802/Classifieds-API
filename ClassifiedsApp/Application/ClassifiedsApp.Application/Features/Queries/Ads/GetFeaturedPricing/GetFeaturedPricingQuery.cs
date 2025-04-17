using ClassifiedsApp.Application.Common.Results;
using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Ads.GetFeaturedPricing;

public class GetFeaturedPricingQuery : IRequest<Result<GetFeaturedPricingQueryResponse>>
{
}
