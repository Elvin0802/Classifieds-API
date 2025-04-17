using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Dtos.Auth.Token;
using ClassifiedsApp.Application.Interfaces.Services.Auth;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Auth.RefreshTokenLogin;

public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommand, Result<AuthTokenDto>>
{
	readonly IAuthService _authService;

	public RefreshTokenLoginCommandHandler(IAuthService authService)
	{
		_authService = authService;
	}

	public async Task<Result<AuthTokenDto>> Handle(RefreshTokenLoginCommand request, CancellationToken cancellationToken)
	{
		try
		{
			return Result.Success(await _authService.RefreshTokenLoginAsync(request.RefreshToken),
									"Refresh Token Login successfull.");
		}
		catch (Exception ex)
		{
			return Result.Failure<AuthTokenDto>($"Error occoured. {ex.Message}");
		}
	}

}
