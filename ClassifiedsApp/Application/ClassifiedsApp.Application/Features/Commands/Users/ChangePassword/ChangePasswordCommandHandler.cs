using ClassifiedsApp.Core.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ClassifiedsApp.Application.Features.Commands.Users.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordCommandResponse>
{
	readonly UserManager<AppUser> _userManager;

	public ChangePasswordCommandHandler(UserManager<AppUser> userManager)
	{
		_userManager = userManager;
	}

	public async Task<ChangePasswordCommandResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
	{
		try
		{
			if (!request.NewPassword.Equals(request.NewPasswordConfirm))
				throw new ValidationException("Please verify the password exactly.");

			AppUser? user = await _userManager.FindByIdAsync(request.UserId) ??
							throw new ArgumentNullException(nameof(request), $"User with this id: {request.UserId} , Not Found.");

			var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

			if (result.Succeeded)
				await _userManager.UpdateSecurityStampAsync(user);

			return new()
			{
				IsSucceeded = result.Succeeded,
				Message = "Password changed."
			};
		}
		catch (Exception ex)
		{
			return new()
			{
				IsSucceeded = false,
				Message = ex.Message
			};
		}
	}
}
