using ClassifiedsApp.Application.Common.Results;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Users.UnselectAd;

public class UnselectAdCommand : IRequest<Result>
{
	public Guid SelectAdId { get; set; }
}
