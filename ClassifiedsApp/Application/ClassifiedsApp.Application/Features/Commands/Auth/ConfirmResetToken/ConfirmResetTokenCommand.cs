using ClassifiedsApp.Application.Common.Results;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Auth.ConfirmResetToken;

public class ConfirmResetTokenCommand : IRequest<Result>
{
	public string? ResetToken { get; set; }
	public string? UserId { get; set; }
}
