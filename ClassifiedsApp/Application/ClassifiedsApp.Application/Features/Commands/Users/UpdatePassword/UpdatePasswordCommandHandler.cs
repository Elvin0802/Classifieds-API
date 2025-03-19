using ClassifiedsApp.Application.Common.Helpers;
using ClassifiedsApp.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ClassifiedsApp.Application.Features.Commands.Users.UpdatePassword;

public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, UpdatePasswordCommandResponse>
{
	readonly UserManager<AppUser> _userManager;

	public UpdatePasswordCommandHandler(UserManager<AppUser> userManager)
	{
		_userManager = userManager;
	}

	public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
	{
		AppUser? user = await _userManager.FindByIdAsync(request.UserId);

		if (user is not null)
		{
			request.ResetToken = request.ResetToken.UrlDecode();

			var result = await _userManager.ResetPasswordAsync(user, request.ResetToken, request.Password);

			if (result.Succeeded)
				await _userManager.UpdateSecurityStampAsync(user);

			return new()
			{
				IsSucceeded = result.Succeeded
			};
		}

		return new()
		{
			IsSucceeded = false
		};
	}
}
