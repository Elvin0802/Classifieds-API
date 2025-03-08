using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Ads.DeleteAd;

public class DeleteAdCommand : IRequest<DeleteAdCommandResponse>
{
	public Guid Id { get; set; }
}
