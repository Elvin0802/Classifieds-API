using ClassifiedsApp.Application.Common.Results;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Ads.FeatureAd;

public class FeatureAdCommand : IRequest<Result>
{
	public Guid AdId { get; set; }
	public int DurationDays { get; set; }
}
