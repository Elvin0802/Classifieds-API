using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Users.UnselectAd;

public class UnselectAdCommand : IRequest<UnselectAdCommandResponse>
{
	public Guid SelectorAppUserId { get; set; }
	public Guid SelectAdId { get; set; }
}
