using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Interfaces.Services.Auth;
using MediatR;
using System.Security;

namespace ClassifiedsApp.Application.Features.Commands.Auth.ConfirmResetToken;

public class ConfirmResetTokenCommandHandler : IRequestHandler<ConfirmResetTokenCommand, Result>
{
	readonly IAuthService _authService;

	public ConfirmResetTokenCommandHandler(IAuthService authService)
	{
		_authService = authService;
	}

	public async Task<Result> Handle(ConfirmResetTokenCommand request, CancellationToken cancellationToken)
	{
		try
		{
			if (await _authService.ConfirmResetTokenAsync(request.UserId!, request.ResetToken!))
				return Result.Success("Reset token confirmed.");

			throw new VerificationException("Reset token not confirmed.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error occoured. {ex.Message}");
		}
	}
}
