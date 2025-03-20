using ClassifiedsApp.Application.Common.Helpers;
using ClassifiedsApp.Core.Entities;
using FluentValidation;
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
		try
		{
			if (!request.Password.Equals(request.PasswordConfirm))
				throw new ValidationException("Please verify the password exactly.");

			AppUser? user = await _userManager.FindByIdAsync(request.UserId) ??
							throw new ArgumentNullException(nameof(request), $"User with this id: {request.UserId} , Not Found.");

			request.ResetToken = request.ResetToken.UrlDecode();

			var result = await _userManager.ResetPasswordAsync(user, request.ResetToken, request.Password);

			if (result.Succeeded)
				await _userManager.UpdateSecurityStampAsync(user);

			return new()
			{
				IsSucceeded = result.Succeeded,
				Message = "Password updated."
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
