using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Interfaces.Services.Users;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Auth.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
{
	private readonly IUserService _userService;

	public RegisterCommandHandler(IUserService userService)
	{
		_userService = userService;
	}

	public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
	{
		try
		{
			if (await _userService.CreateAsync(request.CreateAppUserDto))
				return Result.Success("User registered successfully.");

			throw new Exception("User register failed.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error occoured. {ex.Message}");
		}
	}
}
