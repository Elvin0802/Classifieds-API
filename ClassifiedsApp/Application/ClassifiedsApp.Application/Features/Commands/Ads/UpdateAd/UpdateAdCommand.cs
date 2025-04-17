using ClassifiedsApp.Application.Common.Results;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Ads.UpdateAd;

public class UpdateAdCommand : IRequest<Result>
{
	public Guid Id { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	public bool IsNew { get; set; }
}
