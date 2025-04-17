using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Dtos.Auth.Token;
using ClassifiedsApp.Application.Interfaces.Services.Auth;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Auth.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthTokenDto>>
{
	readonly IAuthService _authService;

	public LoginCommandHandler(IAuthService authService)
	{
		_authService = authService;
	}

	public async Task<Result<AuthTokenDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
	{
		try
		{
			return Result.Success(await _authService.LoginAsync(request.Email, request.Password),
									"Login successful.");
		}
		catch (Exception ex)
		{
			return Result.Failure<AuthTokenDto>($"Error occoured. {ex.Message}");
		}
	}
}
