using ClassifiedsApp.Application.Interfaces.Services.Auth;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Auth.RefreshTokenLogin;

public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommand, RefreshTokenLoginCommandResponse>
{
	readonly IAuthService _authService;

	public RefreshTokenLoginCommandHandler(IAuthService authService)
	{
		_authService = authService;
	}

	public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommand request, CancellationToken cancellationToken)
	{
		return new() { AuthToken = await _authService.RefreshTokenLoginAsync(request.RefreshToken) };
	}

}
