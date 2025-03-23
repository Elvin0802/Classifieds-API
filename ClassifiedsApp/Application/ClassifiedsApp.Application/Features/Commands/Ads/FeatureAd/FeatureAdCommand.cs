using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Ads.FeatureAd;

public class FeatureAdCommand : IRequest<FeatureAdCommandResponse>
{
	public Guid AdId { get; set; }
	public Guid AppUserId { get; set; }
	public int DurationDays { get; set; }
}
