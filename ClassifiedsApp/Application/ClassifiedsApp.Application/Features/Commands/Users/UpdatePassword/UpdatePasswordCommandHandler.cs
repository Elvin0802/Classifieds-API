using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Interfaces.Services.Users;
using FluentValidation;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Users.UpdatePassword;

public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, Result>
{
	readonly IUserService _userService;

	public UpdatePasswordCommandHandler(IUserService userService)
	{
		_userService = userService;
	}

	public async Task<Result> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
	{
		try
		{
			if (!request.Password.Equals(request.PasswordConfirm))
				throw new ValidationException("Please verify the password exactly.");

			if (!await _userService.UpdatePasswordAsync(request.UserId, request.ResetToken, request.Password))
				throw new Exception("Password Not Updated.");

			return Result.Success("Password updated.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error occoured. {ex.Message}");
		}
	}
}
