using ClassifiedsApp.Application.Interfaces.Services.Users;
using FluentValidation;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Users.UpdatePassword;

public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, UpdatePasswordCommandResponse>
{
	readonly IUserService _userService;

	public UpdatePasswordCommandHandler(IUserService userService)
	{
		_userService = userService;
	}

	public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
	{
		try
		{
			if (!request.Password.Equals(request.PasswordConfirm))
				throw new ValidationException("Please verify the password exactly.");

			if (!await _userService.UpdatePasswordAsync(request.UserId, request.ResetToken, request.Password))
				throw new Exception("Password Not Updated.");

			return new()
			{
				IsSucceeded = true,
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
