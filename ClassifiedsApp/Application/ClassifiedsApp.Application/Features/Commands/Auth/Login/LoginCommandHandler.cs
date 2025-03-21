using ClassifiedsApp.Application.Interfaces.Services.Auth;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Auth.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
{
	readonly IAuthService _authService;

	public LoginCommandHandler(IAuthService authService)
	{
		_authService = authService;
	}

	public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
	{
		return new LoginCommandResponse()
		{
			AuthToken = await _authService.LoginAsync(request.Email, request.Password)
		};
	}
}
