using ClassifiedsApp.Application.Interfaces.Services.Users;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Auth.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterCommandResponse>
{
	private readonly IUserService _userService;

	public RegisterCommandHandler(IUserService userService)
	{
		_userService = userService;
	}

	public async Task<RegisterCommandResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
	{
		if (await _userService.CreateAsync(request.CreateAppUserDto))
			return new()
			{
				IsSucceeded = true,
				Message = "User registered successfully."
			};

		return new()
		{
			IsSucceeded = false,
			Message = $"User not registered."
		};
	}
}
