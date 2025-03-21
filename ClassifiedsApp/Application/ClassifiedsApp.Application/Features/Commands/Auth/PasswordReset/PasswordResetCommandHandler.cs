using ClassifiedsApp.Application.Interfaces.Services.Auth;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Auth.PasswordReset;

public class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommand, PasswordResetCommandResponse>
{
	readonly IAuthService _authService;

	public PasswordResetCommandHandler(IAuthService authService)
	{
		_authService = authService;
	}

	public async Task<PasswordResetCommandResponse> Handle(PasswordResetCommand request, CancellationToken cancellationToken)
	{
		return new() { IsSucceeded = (await _authService.PasswordResetAsnyc(request.Email)) };
	}
}
