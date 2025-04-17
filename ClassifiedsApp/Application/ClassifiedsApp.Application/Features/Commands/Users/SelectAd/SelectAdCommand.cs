using ClassifiedsApp.Application.Common.Results;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Users.SelectAdCommand;

public class SelectAdCommand : IRequest<Result>
{
	public Guid SelectAdId { get; set; }
}
