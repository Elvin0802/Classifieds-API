using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Interfaces.Services.Users;
using FluentValidation;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Users.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
{
	readonly IUserService _userService;

	public ChangePasswordCommandHandler(IUserService userService)
	{
		_userService = userService;
	}

	public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
	{
		try
		{
			if (!request.NewPassword.Equals(request.NewPasswordConfirm))
				throw new ValidationException("Please verify the password exactly.");

			if (!await _userService.ChangePasswordAsync(request.UserId, request.OldPassword, request.NewPassword))
				throw new Exception("Change Password failed.");

			return Result.Success("Password changed successfully.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error occoured. {ex.Message}");
		}
	}
}
