using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Users.SelectAdCommand;

public class SelectAdCommand : IRequest<SelectAdCommandResponse>
{
	public Guid SelectorAppUserId { get; set; }
	public Guid SelectAdId { get; set; }
}
