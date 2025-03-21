using ClassifiedsApp.Application.Interfaces.Services.Users;
using FluentValidation;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Users.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordCommandResponse>
{
	readonly IUserService _userService;

	public ChangePasswordCommandHandler(IUserService userService)
	{
		_userService = userService;
	}

	public async Task<ChangePasswordCommandResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
	{
		try
		{
			if (!request.NewPassword.Equals(request.NewPasswordConfirm))
				throw new ValidationException("Please verify the password exactly.");

			if (!await _userService.ChangePasswordAsync(request.UserId, request.OldPassword, request.NewPassword))
				throw new Exception("Change Password failed.");

			return new()
			{
				IsSucceeded = true,
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
