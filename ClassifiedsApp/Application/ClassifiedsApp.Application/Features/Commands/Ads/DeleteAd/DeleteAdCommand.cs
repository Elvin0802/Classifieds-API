using ClassifiedsApp.Application.Common.Results;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Ads.DeleteAd;

public class DeleteAdCommand : IRequest<Result>
{
	public Guid Id { get; set; }
}
