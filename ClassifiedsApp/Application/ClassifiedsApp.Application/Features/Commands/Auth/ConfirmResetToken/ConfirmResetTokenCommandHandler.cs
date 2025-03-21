using ClassifiedsApp.Application.Interfaces.Services.Auth;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Auth.ConfirmResetToken;

public class ConfirmResetTokenCommandHandler : IRequestHandler<ConfirmResetTokenCommand, ConfirmResetTokenCommandResponse>
{
	readonly IAuthService _authService;

	public ConfirmResetTokenCommandHandler(IAuthService authService)
	{
		_authService = authService;
	}

	public async Task<ConfirmResetTokenCommandResponse> Handle(ConfirmResetTokenCommand request, CancellationToken cancellationToken)
	{
		return new()
		{
			IsSucceeded = await _authService.ConfirmResetTokenAsync(request.UserId!, request.ResetToken!)
		};
	}
}
