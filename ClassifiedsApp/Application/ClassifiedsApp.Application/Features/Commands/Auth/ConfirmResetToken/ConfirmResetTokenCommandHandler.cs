using ClassifiedsApp.Application.Common.Helpers;
using ClassifiedsApp.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ClassifiedsApp.Application.Features.Commands.Auth.ConfirmResetToken;

public class ConfirmResetTokenCommandHandler : IRequestHandler<ConfirmResetTokenCommand, ConfirmResetTokenCommandResponse>
{
	readonly UserManager<AppUser> _userManager;

	public ConfirmResetTokenCommandHandler(UserManager<AppUser> userManager)
	{
		_userManager = userManager;
	}

	public async Task<ConfirmResetTokenCommandResponse> Handle(ConfirmResetTokenCommand request, CancellationToken cancellationToken)
	{
		AppUser? user = await _userManager.FindByIdAsync(request.UserId);

		if (user is not null && !(string.IsNullOrEmpty(request.ResetToken)))
		{
			request.ResetToken = request.ResetToken.UrlDecode();

			return new()
			{
				IsSucceeded = await _userManager.VerifyUserTokenAsync(user,
															_userManager.Options.Tokens.PasswordResetTokenProvider,
															"ResetPassword",
															request.ResetToken)
			};
		}

		return new()
		{
			IsSucceeded = false
		};
	}
}
