using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Interfaces.Services.Auth;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Auth.PasswordReset;

public class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommand, Result>
{
	readonly IAuthService _authService;

	public PasswordResetCommandHandler(IAuthService authService)
	{
		_authService = authService;
	}

	public async Task<Result> Handle(PasswordResetCommand request, CancellationToken cancellationToken)
	{
		try
		{
			if (await _authService.PasswordResetAsnyc(request.Email))
				return Result.Success("Password reset successfully completed.");

			throw new Exception("Password reset failed.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error occoured. {ex.Message}");
		}
	}
}
